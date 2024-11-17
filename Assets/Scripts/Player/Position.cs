using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    private Trap trap;
    public float updateInterval = 5f; // Intervalo de actualización (en segundos)
    private Vector3 lastGroundedPosition; // Última posición registrada en el suelo
    private float timer; // Temporizador para controlar las actualizaciones
    private bool isGrounded; // Indica si el jugador está en el suelo

    private void Start()
    {
        trap = FindObjectOfType<Trap>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval && isGrounded)
        {
            // Actualiza la posición si el jugador está en el suelo
            lastGroundedPosition = transform.position;
            timer = 0f; // Reinicia el temporizador
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Comprueba si el jugador está en contacto con el suelo
        if (collision.gameObject.CompareTag("Ground"))        
            isGrounded = true;        
    }

    private void OnCollisionExit(Collision collision)
    {
        // Marca que el jugador ya no está tocando el suelo
        if (collision.gameObject.CompareTag("Ground"))        
            isGrounded = false;        
    }

    public Vector3 GetLastGroundedPosition()
    {
        return lastGroundedPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Comprueba si el jugador está tocando el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reactiva las paredes invisibles
            if (trap != null)
                trap.ToggleInvisibleWalls(true);            
        }
    }


}
