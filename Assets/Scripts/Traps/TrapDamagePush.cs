using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage = 10f;
    public float pushForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth hl = collision.gameObject.GetComponent<PlayerHealth>();
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (hl != null)
        {
            hl.Damage(damage);

            rb.AddForce(transform.forward * pushForce, ForceMode.Force);           
        }
    }
}