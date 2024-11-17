using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    public float damage = 10f;
    //public float pushForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (other is CapsuleCollider)
        {
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
            }
        }
        //Rigidbody playerRb = other.GetComponent<Rigidbody>();
        //if (playerRb != null)
        //{
        //    Vector3 pushDirection = (other.transform.position - transform.position).normalized;
        //    playerRb.AddForce(pushDirection * pushForce, ForceMode.VelocityChange);
        //}
    }
}