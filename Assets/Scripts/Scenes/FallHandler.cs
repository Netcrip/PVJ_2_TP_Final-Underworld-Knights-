using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica que el objeto es el jugador
        {
            PositionRecorder recorder = other.GetComponent<PositionRecorder>();
            if (recorder != null)
            {
                other.transform.position = recorder.lastSafePosition; // Llevar al jugador a la última posición segura
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
