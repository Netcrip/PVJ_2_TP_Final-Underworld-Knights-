using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresureTraps : MonoBehaviour
{   
    Animator anim;
    [SerializeField] float _startDelay=2;
    float delay;

    private void Start() {
        anim= gameObject.GetComponent<Animator>();
    }
     void Update()
    {
        delay+=Time.deltaTime;
        if(delay>_startDelay){
            anim.SetTrigger("Start");
        }
    }
}
