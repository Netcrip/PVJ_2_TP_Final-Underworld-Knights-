using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerHealth : MonoBehaviour, IDamagable
{
    // 
    [SerializeField]float health;
    [SerializeField] float _maxHealth=100;

    public bool playerAlive  {get;private set;} = true;
    private Combat combat;
    private PlayerInput playerInput;
    private Animator anim;
    //Eventos
    public Action onDead;
    public Action onUnsuscribe;
    public Action<float, float> onHealthchange;

    private float delayAnimDamage;
    private float delayAD=0.6f;

    PlayerSFX playerSFX;
    void Awake()
    {
        UiManager.Instance.PlayerHealth(this);
        PlayerManager.Instance.PlayerHealth(this);
        combat = GetComponent<Combat>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        if(PlayerManager.Instance.playerHealth==0){
            health = _maxHealth;
            onHealthchange?.Invoke(health, _maxHealth);
        }
        else{
            health=PlayerManager.Instance.playerHealth;
            _maxHealth=PlayerManager.Instance.playerMaxHealth;
            onHealthchange?.Invoke(health, _maxHealth);
        }
        playerSFX =GetComponent<PlayerSFX>();

    }

    private void Update() {
        delayAnimDamage+=Time.deltaTime;
    }

    // Update is called once per frame
    public void Damage(float damageAmount) { 
        if(playerAlive)
            GetHit(damageAmount);
    }

    private void GetHit(float damageAmount)
    {   
        if (combat.Defence)
        {
            if(delayAnimDamage>delayAD){
                anim.SetTrigger("Defence");
                playerSFX.PlaySFX("defence");
                delayAnimDamage=Time.deltaTime;
            }
            health -= (damageAmount/2);
            onHealthchange?.Invoke(health,_maxHealth);
        }
        else
        {
            if(delayAnimDamage>delayAD){
                anim.SetTrigger("Damage");
                playerSFX.PlaySFX("hit");
                delayAnimDamage=Time.deltaTime;
            }
            health -= damageAmount;
            onHealthchange?.Invoke(health, _maxHealth);
        }
            

        if(health <= 0)
        {
            playerAlive=false;
            anim.SetTrigger("Die");
            playerSFX.PlaySFX("die");
            health = 0;
            onDead?.Invoke();
        }
    }
    public void Heal(float healAmount)
    {         
        if (health + healAmount <= _maxHealth)
        {
            health += healAmount;
            onHealthchange?.Invoke(health, _maxHealth);
        }
        else
        {
            health = _maxHealth;
            onHealthchange?.Invoke(health, _maxHealth);
        }
    }
    public void Unsuscribe(){
        onUnsuscribe?.Invoke();
    }


}
