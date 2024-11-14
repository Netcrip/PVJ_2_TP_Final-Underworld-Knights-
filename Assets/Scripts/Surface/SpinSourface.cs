using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSourface : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {

        GameObject player = other.gameObject;
        PlayerHealth ph = player.GetComponentInChildren<PlayerHealth>();
        if(ph != null){
            player.transform.parent = this.transform;
        }
        
    }
    private void OnCollisionExit(Collision other) {

        GameObject player = other.gameObject;
        PlayerHealth ph = player.GetComponentInChildren<PlayerHealth>();
        if(ph != null){
            player.transform.parent = null;
        }
        
    }
}
