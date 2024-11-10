using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x : MonoBehaviour
{
    [SerializeField] GameObject fuegito;
    [SerializeField] Collider col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fuegito.activeSelf){
           col.enabled=true; 
        }
        else
             col.enabled=false; 
    }
}
