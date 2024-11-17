using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageTime : MonoBehaviour
{
    public float damage = 10f; // Daño por golpe al jugador
    private float delay;
    [SerializeField] private float delayCD = 5f;


    //private void OnTriggerEnter(Collider other)
    //{
    //    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
    //    if (other is CapsuleCollider)
    //    {
    //        if (playerHealth != null)
    //        {
    //            playerHealth.Damage(damage);
    //        }
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        // Comprueba si el objeto con el que colisionó tiene un componente PlayerHealth
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        //delay += Time.deltaTime;
        //if (delay > delayCD)
        //{
            // Llamado al metodo que aplica el daño
            ApllyDamage(playerHealth);

            // Reseta el delay
            //delay = Time.deltaTime;
        //}
    }

    public void ApllyDamage(PlayerHealth playerHealth)
    {
        if (playerHealth != null)
            playerHealth.Damage(damage * Time.deltaTime);
    }
}
