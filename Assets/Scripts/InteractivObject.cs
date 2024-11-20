using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivObject : MonoBehaviour
{
    // Start is called before the first frame update
   
    [SerializeField] Canvas _canvas;
    
    void Start()
    {
        _canvas.enabled = false;
    }
    // Update is called once per frame

   private void OnTriggerEnter(Collider other) {

    if(other.CompareTag("Player")){
        _canvas.enabled=true;        
    }
   
   }

 
   private void OnTriggerExit(Collider other) {
    if(other.CompareTag("Player")){
        _canvas.enabled=false;
    }
   }
}
