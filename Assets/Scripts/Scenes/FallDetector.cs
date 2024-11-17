using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private Position playerTracker;

    private void Start()
    {
        // Encuentra el script PlayerPositionTracker en el jugador
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTracker = player.GetComponent<Position>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador toca este collider
        if (other.CompareTag("Player") && playerTracker != null)
        {
            // Teletransporta al jugador a la última posición registrada en el suelo
            other.transform.position = playerTracker.GetLastGroundedPosition();
        }
    }
}
