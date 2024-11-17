using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Vector3 respawnPoint;
    private void Start() {
        respawnPoint=transform.position;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            GameManager.Instance.SetRespanw(respawnPoint);
            this.gameObject.SetActive(false);
        }
    }
}
