using UnityEngine;


    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerStamina))]
    public class Movement : MonoBehaviour
    {
        private Animator _animator;
        private PlayerInput _playerInput;
        private Combat _combat;
        private CharacterController _characterController;
        private PlayerStamina _playerStamina;

        private Vector2 lastMovementInput;
        [SerializeField ]private Vector3 moveDirection = Vector3.zero;

        [SerializeField] float _gravity = 10;
        [SerializeField] float _jumpSpeed = 4;
        [SerializeField] float _maxSpeed = 10;
        [SerializeField] float _dashStamina = 10;
        float speed;

        private float DecelerationOnStop = 0.05f;  // Ajuste de desaceleración gradual

        [SerializeField]public float rotationSpeed=5f; // Velocidad de rotación del personaje
       //[SerializeField] private Quaternion currentRotation;
  
        [SerializeField] private Vector3 currentRotation;


        PlayerSFX playerSFX;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _combat = GetComponent<Combat>();
            _characterController = GetComponent<CharacterController>();
            currentRotation= _characterController.transform.eulerAngles;
            _playerStamina = GetComponent<PlayerStamina>();
            speed = _maxSpeed;
            playerSFX=GetComponent<PlayerSFX>();

        }

        private void Update()
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
                    Dash();
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
                moveDirection = transform.TransformDirection(moveDirection.normalized)*_maxSpeed;

                if (_playerInput.JumpInput)
                {
                    moveDirection.y = _jumpSpeed;
                    _animator.SetTrigger("Jump");
                    playerSFX.playSFX("jump");
                }
                    
            }

            moveDirection.y -= _gravity * Time.deltaTime;
            _characterController.Move(moveDirection * Time.deltaTime);


            
            if(_maxSpeed > speed) {
                _animator.SetFloat("InputY", y*2);
                playerSFX.playSFX("dash");
            }
            else{}
                _animator.SetFloat("InputY", y);
            _animator.SetFloat("InputX", x);
            if(x!=0 || y!=0)
                playerSFX.playSFX("move");
            _animator.SetBool("IsInAir", !grounded);
        }
       
        private void RotateWithMouse()
        {
            if(_playerInput.PlayerRotate)
            {
                
                 float mouseX = Input.GetAxis("Mouse X");

                if(mouseX != 0f)
                {
                    //_characterController.transform.Rotate(0, mouseX * rotationSpeed, 0);
                    currentRotation = new Vector3 (0,currentRotation.y+mouseX,0);
                    //guardo
                }
                
           
            }
         
         _characterController.transform.eulerAngles = currentRotation;

        }


        private void StopMovementOnAttack()
        {
            // Suaviza la desaceleración hacia cero usando Lerp
            lastMovementInput.x = Mathf.Lerp(lastMovementInput.x, 0, DecelerationOnStop);
            lastMovementInput.y = Mathf.Lerp(lastMovementInput.y, 0, DecelerationOnStop);

            _animator.SetFloat("InputX", lastMovementInput.x);
            _animator.SetFloat("InputY", lastMovementInput.y);
            
        }

        private void Dash()
        {
            if(_playerInput.Dash && (moveDirection.x !=0f || moveDirection.z !=0f))
            {
                if (_playerStamina.StaminaUse(_dashStamina,true))
                {
                    //_playerStamina.CanStaminaRegeneration=false;
                    _maxSpeed = speed * 1.5f;
                }
                else
                    _maxSpeed = speed;

            }
            else
            {
               _maxSpeed = speed;
               //_playerStamina.CanStaminaRegeneration = true;
            }
        }
    }
