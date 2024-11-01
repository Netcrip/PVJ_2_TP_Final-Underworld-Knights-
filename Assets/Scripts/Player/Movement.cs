using UnityEngine;

namespace Retro.ThirdPersonCharacter
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        private Animator _animator;
        private PlayerInput _playerInput;
        private Combat _combat;
        private CharacterController _characterController;

        private Vector2 lastMovementInput;
        private Vector3 moveDirection = Vector3.zero;

        public float gravity = 10;
        public float jumpSpeed = 4;
        public float MaxSpeed = 10;
        private float DecelerationOnStop = 0.05f;  // Ajuste de desaceleración gradual

        [SerializeField]public float rotationSpeed=5f; // Velocidad de rotación del personaje
        [SerializeField] private Quaternion currentRotation;
  



        private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _combat = GetComponent<Combat>();
            _characterController = GetComponent<CharacterController>();
             currentRotation= transform.rotation;
        }

        private void FixedUpdate()
        {
            if (_animator == null) return;

            RotateWithMouse();

            if (_combat.AttackInProgress)
            {
                StopMovementOnAttack();
            }
            else
            {
                if (_combat.CanMove)
                {
                    Move();
                }
                
            }
        }

     

        private void Move()
        {
            var x = _playerInput.MovementInput.x;
            var y = _playerInput.MovementInput.y;

            // Guardar el último movimiento de entrada cuando no hay ataque en progreso
            lastMovementInput = new Vector2(x, y);

        

            bool grounded = _characterController.isGrounded;

            if (grounded)
            {
                moveDirection = new Vector3(x, 0, y);
                moveDirection = transform.TransformDirection(moveDirection.normalized)*MaxSpeed;

                if (_playerInput.JumpInput)
                {
                    moveDirection.y = jumpSpeed;
                    _animator.SetTrigger("Jump");
                }
                    
            }

            moveDirection.y -= gravity * Time.fixedDeltaTime;
            _characterController.Move(moveDirection * Time.fixedDeltaTime);

            _animator.SetFloat("InputX", x);
            _animator.SetFloat("InputY", y);
            _animator.SetBool("IsInAir", !grounded);
        }
       
        private void RotateWithMouse()
        {


            if(_playerInput.PlayerRotate)
            {
                
                 float mouseX = Input.GetAxis("Mouse X");

                if(mouseX != 0f)
                {
                    _characterController.transform.Rotate(0, mouseX * rotationSpeed, 0);
                    //guardo
                    currentRotation = transform.rotation;
                }
                
           
            }
         
         _characterController.transform.rotation = currentRotation;
   

        }


        private void StopMovementOnAttack()
        {
            // Suaviza la desaceleración hacia cero usando Lerp
            lastMovementInput.x = Mathf.Lerp(lastMovementInput.x, 0, DecelerationOnStop);
            lastMovementInput.y = Mathf.Lerp(lastMovementInput.y, 0, DecelerationOnStop);

            _animator.SetFloat("InputX", lastMovementInput.x);
            _animator.SetFloat("InputY", lastMovementInput.y);
            
        }
    }
}