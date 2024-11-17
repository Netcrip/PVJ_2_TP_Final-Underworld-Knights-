using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageTime : MonoBehaviour
{
    public float damagePerSecond = 5f;

    private bool playerInZone = false;
    private PlayerHealth playerHealth;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerInZone = true;
            damageCoroutine = StartCoroutine(ApplyDamageOverTime());
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerInZone)
        {
            playerInZone = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        while (playerInZone)
        {
            playerHealth.Damage(damagePerSecond * Time.deltaTime);
            yield return null;
        }
    }
}
