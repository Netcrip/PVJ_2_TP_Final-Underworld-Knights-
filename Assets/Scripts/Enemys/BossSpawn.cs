using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] GameObject _vfx;
    [SerializeField] GameObject _boss;
    [SerializeField] float _delay;
    [SerializeField] AudioClip _Areasfx;
    [SerializeField] AudioClip _Bosssfx;
    private float delay;
    private bool doOne =true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delay+=Time.deltaTime;
        if(delay >_delay){

            _boss.SetActive(true);
            _vfx.SetActive(false);
            //Grito del Boss
        }
        if(doOne){
            doOne=false;
            //sonido del portal
        }
    }
}
