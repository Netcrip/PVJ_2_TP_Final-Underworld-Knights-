using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPosition : MonoBehaviour
{
    [SerializeField] Transform initialPosition;
    void Start()
    {
        GameManager.Instance.SetRespanw(initialPosition.position);
        
        CharacterController player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        GameObject avatar = GameObject.FindGameObjectWithTag("Avatar");
        if (player != null)
        {
            player.enabled = false; // Desactivar el CharacterController temporalmente
            avatar.transform.position = GameManager.Instance.respawn;
            player.enabled = true; // Reactivarlo
        }
    }

}
