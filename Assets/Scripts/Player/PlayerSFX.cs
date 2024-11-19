using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour, IPlaySFX
{

    [SerializeField] AudioSource _move;
    [SerializeField] AudioClip _attack;
    [SerializeField] AudioClip _attack2;
    [SerializeField] AudioClip _Other;
    [SerializeField] AudioClip _die;
    [SerializeField] AudioClip _hit;
    [SerializeField] AudioClip _jump;
    [SerializeField] AudioClip _defence;

    [SerializeField] private AudioSource sfx;


    public void PlaySFX(string state){
        switch (state){
            case "move":
            if (!_move.isPlaying)
            {
                _move.enabled=true;
                _move.pitch = 1f;
            }
            _move.enabled=true;
            break;
            case "attack":
            sfx.PlayOneShot(_attack);
            break;
            case "attack2":
            sfx.PlayOneShot(_attack2);
            break;
            case "dash":       
            if (!_move.isPlaying)
            {
                _move.enabled=true;
                _move.pitch = 1.5f;
            }
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
            case "stomMove":
            {
            if (_move.isPlaying)
                _move.enabled=false;
            }
            break;
            case "shield":
            sfx.PlayOneShot(_Other);
            break;
            
        }
    }

    

}
