using UnityEngine;


namespace Retro.ThirdPersonCharacter
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    public class Combat : MonoBehaviour
    {
        private const string attackTriggerName = "Attack";
        private const string specialAttackTriggerName = "SpecialAttack";
        private const string defenceBoolName = "Block";

        private Animator _animator;
        private PlayerInput _playerInput;

        public bool AttackInProgress {get; private set;} = false;
        public bool CanMove { get; private set;} = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
           
           
             Block(_playerInput.Block);
            
             if(_playerInput.AttackInput && !AttackInProgress)
            {
                Attack();
            }
            else if (_playerInput.SpecialAttackInput && !AttackInProgress)
            {
                SpecialAttack();
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
            _animator.SetTrigger(attackTriggerName);
        }

        private void SpecialAttack()
        {
            _animator.SetTrigger(specialAttackTriggerName);
        }
        private void Block(bool block)
        {
            _animator.SetBool(defenceBoolName, block);
            CanMove = !block;
        }
    }
}