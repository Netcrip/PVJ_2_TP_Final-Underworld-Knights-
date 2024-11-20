using System;
using Unity.VisualScripting;
using UnityEngine;




    [RequireComponent(typeof(PlayerInput))]
   [RequireComponent(typeof(PlayerStamina))]
    [RequireComponent(typeof(Animator))]
    public class Combat : MonoBehaviour
    {
        private const string attackTriggerName = "Attack";
        private const string specialAttackTriggerName = "SpecialAttack";
        private const string defenceBoolName = "Block";

        private Animator animator;
        private PlayerInput playerInput;

        private PlayerStamina playerStamina;

        public bool AttackInProgress {get; private set;} = false;
        public bool CanMove { get; private set;} = true;

        public bool Defence { get; private set; } = false;

        [SerializeField] BoxCollider weapon, shield;
        BoxCollider[] colliders;

        private float delayAttack;
        private float delayAttackSheild;

        [SerializeField]private float delayToAttack=0.7f;
        [SerializeField]private float delayToAttackShield=4f;
        PlayerHealth playerHealth;

        PlayerSFX playerSFX;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
            colliders= this.GetComponentsInChildren<BoxCollider>();
            playerStamina = GetComponent<PlayerStamina>();
            foreach(BoxCollider b in colliders){
                if(b.CompareTag("Weapon"))
                    weapon =b;
                else if (b.CompareTag("Shield"))
                    shield=b;
            }
            weapon.enabled=false;
            shield.enabled=false;
            playerSFX = GetComponent<PlayerSFX>();
            playerHealth= GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            if(playerHealth.playerAlive)
            {
                delayAttack+=Time.deltaTime;
                delayAttackSheild+=Time.deltaTime;
           
                Block(playerInput.Block);
            
                if(playerInput.AttackInput && delayAttack>=delayToAttack)// !AttackInProgress)
                {
                    Attack();
                    
                    delayAttack=Time.deltaTime;
                }
                else if (playerInput.SpecialAttackInput && delayAttackSheild>=delayToAttackShield)//!AttackInProgress)
                {
                    if(playerStamina.StaminaUse(25f,false)){
                        SpecialAttack();                   
                        delayAttackSheild=Time.deltaTime;
                    }
                        
                }
            }
           
        }

        private void SetAttackStart()
        {
            AttackInProgress = true;
        }

        private void SetAttackEnd()
        {
            AttackInProgress = false;
        }

        private void Attack()
        {
            weapon.enabled=true;
            animator.SetTrigger(attackTriggerName);
            playerSFX.PlaySFX("attack");
            Invoke(nameof(DisabledWeponCollider),1f);
        }

        private void SpecialAttack()
        {
            shield.enabled=true;
            animator.SetTrigger(specialAttackTriggerName);
            playerSFX.PlaySFX("attack2");
            Invoke(nameof(DisabledShieldCollider),1f);
        }
        private void Block(bool block)
        {
            animator.SetBool(defenceBoolName, block);
            if(block && !Defence)
                playerSFX.PlaySFX("shield");
            CanMove = !block;
            Defence = block;
        }

        private void OnTriggerStay(Collider other) {
            
        }
         private void OnTriggerEnter(Collider other)
        {
            EnemyHealth eHealth = other.GetComponent<EnemyHealth>();
            if (weapon.enabled && eHealth!=null)
            {
                eHealth.Damage(10f);
                weapon.enabled=false;
            }
            else if (shield.enabled && eHealth!=null)
            {                
                eHealth.StunOn();
                shield.enabled=false;
            }
            
        }
        private void DisabledWeponCollider(){
            weapon.enabled=false;
        }
        private void DisabledShieldCollider(){
            shield.enabled=false;
        }
    }
