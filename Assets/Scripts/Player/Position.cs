using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    private Trap trap;

    private void Start()
    {
        trap = FindObjectOfType<Trap>();
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
