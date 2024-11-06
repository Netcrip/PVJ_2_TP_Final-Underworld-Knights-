using UnityEngine;

namespace Retro.ThirdPersonCharacter
{
    public class PlayerInput : MonoBehaviour
    {
        private bool _attackInput;
        private bool _specialAttackInput;
        private Vector2 _movementInput;
        private bool _jumpInput;
        private bool _playerRotate;
        private bool _block;


        public bool AttackInput {get => _attackInput;}
        public bool SpecialAttackInput {get => _specialAttackInput;}
        public Vector2 MovementInput {get => _movementInput;}
        public bool JumpInput { get => _jumpInput; }
        public bool PlayerRotate { get => _playerRotate; }

        public bool Block { get => _block;}
 

        private void Update()
        {
            _attackInput = Input.GetMouseButtonDown(0);
            _specialAttackInput = Input.GetKeyDown(KeyCode.C);
            _movementInput.Set(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            _jumpInput = Input.GetButton("Jump");
            _playerRotate= Input.GetMouseButton(1);
            _block = Input.GetKey(KeyCode.F);
   
        }
    }
}