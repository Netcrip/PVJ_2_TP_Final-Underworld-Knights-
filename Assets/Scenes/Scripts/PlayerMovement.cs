using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed = 6.0f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3.0f;  // Altura del salto
    [SerializeField] float mouseSensitivity = 40.0f;  // Sensibilidad del mouse
    [SerializeField] float rotationSmoothTime = 0.1f;  // Suavizado de la rotación
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] bool grounded;

    private Vector3 velocity;
    private float currentRotationY;
    private float rotationYVelocity;

    void Start()
    {
        // Bloquear el cursor en el centro de la pantalla y ocultarlo
        Cursor.lockState = CursorLockMode.Locked;
        currentRotationY = transform.eulerAngles.y;  // Inicializa la rotación Y actual
    }

    void Update()
    {
        // Detectar si el personaje está en el suelo
        grounded = controller.isGrounded;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f; // Resetear la velocidad en Y cuando está en el suelo
            animator.SetBool("jump", false); // Desactivar la animación de salto
        }

        if (grounded && Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 1.5f;
            animator.SetBool("dash", true);  // Activar la animación de dash
        }
        else
        {
            speed /= 1.5f;
            animator.SetBool("Dash", true);
        }


        // Movimiento basado en el input del teclado
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(transform.TransformDirection(move) * speed * Time.deltaTime);

        // Control de la animación basada en la velocidad
        float moveMagnitude = move.magnitude;
        animator.SetFloat("move", moveMagnitude);  // Actualiza el parámetro "move"

        // Obtener input de rotación del mouse (horizontal)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Suavizar la rotación horizontal usando Lerp
        currentRotationY += mouseX;  // Acumular la rotación en Y (solo horizontal)
        float targetRotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, currentRotationY, ref rotationYVelocity, rotationSmoothTime);
        transform.eulerAngles = new Vector3(0f, targetRotationY, 0f);  // Aplicar rotación solo en el eje Y

        // Salto con la barra espaciadora
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // Fórmula para calcular la velocidad del salto
            animator.SetBool("jump", true);  // Activar la animación de salto
        }

       

        // Aplicación de gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
