using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    
   private void OnTriggerEnter(Collider other) {
         
        if (other.gameObject.CompareTag("Player"))
    {

        CharacterController controller = other.gameObject.GetComponent<CharacterController>();
        
        if (controller != null)
        {
            controller.enabled = false; // Desactivar el CharacterController temporalmente
            other.gameObject.transform.position = GameManager.Instance.respawn;
            controller.enabled = true; // Reactivarlo
        }

    }


    }
    
}
