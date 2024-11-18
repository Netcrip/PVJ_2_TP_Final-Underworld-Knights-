using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{

    [SerializeField] AudioClip _move;
    [SerializeField] AudioClip _attack;
    [SerializeField] AudioClip _attack2;
    [SerializeField] AudioClip _dash;
    [SerializeField] AudioClip _die;
    [SerializeField] AudioClip _hit;
    [SerializeField] AudioClip _jump;
    [SerializeField] AudioClip _defence;

    private AudioSource sfx;

    private void Start() {
        sfx= GetComponent<AudioSource>();
    }

    public void playSFX(string state){

        switch (state){
            case "move":
            if(!sfx.isPlaying)
                sfx.PlayOneShot(_move);
            break;
            case "attack":
            sfx.PlayOneShot(_attack);
            break;
            case "attack2":
            sfx.PlayOneShot(_attack2);
            break;
            case "dash":
            sfx.PlayOneShot(_dash);
            break;
            case "die":
            sfx.PlayOneShot(_die);
            break;
            case "hit":
            sfx.PlayOneShot(_hit);
            break;
            case "jump":
            sfx.PlayOneShot(_jump);
            break;
            case "defence":
            sfx.PlayOneShot(_defence);
            break;
        }
    }

    

}
