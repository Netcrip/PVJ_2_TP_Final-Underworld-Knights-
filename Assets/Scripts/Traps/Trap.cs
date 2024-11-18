using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private CharacterController _characterController;
    public float damage = 10f; // Daño por golpe al jugador
    private float pushForce = 50f; // Fuerza inicial del empuje
    private float pushDecay = 5f; // Velocidad a la que el empuje disminuye
    private Vector3 _pushDirection; // Dirección del empuje 

    //public Collider[] invisibleWalls; // Lista de colisionadores de las paredes invisibles

    private void Start()
    {
        _characterController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Si hay una dirección de empuje, mueve el CharacterController
        if (_pushDirection.magnitude > 0.1f)
        {
            // Mueve el CharacterController en la dirección del empuje
            _characterController.Move(_pushDirection * Time.deltaTime);
    
            // Reduce gradualmente el empuje hasta que se detenga
            _pushDirection = Vector3.Lerp(_pushDirection, Vector3.zero, pushDecay * Time.deltaTime);
        }
    }

    public void ApplyPush(Vector3 impactPoint)
    {
        // Calcula la dirección del empuje como el vector desde el punto de impacto hacia el jugador
        Vector3 direction = transform.position - impactPoint;
        _pushDirection = -direction.normalized * pushForce;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Comprueba si el objeto con el que colisionó tiene un componente PlayerHealth
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

        // Obtén el primer punto de contacto
        Vector3 impactPoint = collision.contacts[0].point;

        // Llama a ApplyPush para aplicar el empuje
        ApplyPush(impactPoint);

        // Llamado al metodo que aplica el daño
        ApllyDamage(playerHealth);

        // Desactiva las paredes invisibles
        ToggleInvisibleWalls(false);
    }

    public void ApllyDamage(PlayerHealth playerHealth)
    {
        if (playerHealth != null)        
            playerHealth.Damage(damage);        
    }

    public void ToggleInvisibleWalls(bool isActive)
    {
        GameObject[] invisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWall");
        foreach (GameObject wall in invisibleWalls)
        {
            wall.GetComponent<BoxCollider>().enabled = isActive;
        }
    }
}
