using UnityEngine;



    public class PlayerInput : MonoBehaviour
    {
        private bool _attackInput;
        private bool _specialAttackInput;
        private Vector2 _movementInput;
        private bool _jumpInput;
        private bool _playerRotate;
        private bool _block;
        private bool _dash;




        public bool AttackInput {get => _attackInput;}
        public bool SpecialAttackInput {get => _specialAttackInput;}
        public Vector2 MovementInput {get => _movementInput;}
        public bool JumpInput { get => _jumpInput; }
        public bool PlayerRotate { get => _playerRotate; }

        public bool Block { get => _block;}

        public bool Dash { get => _dash; }


        private void Update()
        {
            _attackInput = Input.GetMouseButtonDown(0);
            _specialAttackInput = Input.GetKeyDown(KeyCode.C);
            _movementInput.Set(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            _jumpInput = Input.GetButtonDown("Jump");
            _playerRotate= Input.GetMouseButton(1);
            _block = Input.GetKey(KeyCode.F);
            _dash = Input.GetKey(KeyCode.LeftShift);

        }
    }
