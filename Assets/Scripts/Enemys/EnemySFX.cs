using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour, IPlaySFX
{
    [SerializeField] private AudioSource _move;

    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _attack2;
    [SerializeField] private AudioClip _die;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _roar;
    [SerializeField] private AudioClip _roar2;
    [SerializeField] private AudioClip _idleRoad;

    [SerializeField] private AudioSource sfx;

    // Start is called before the first frame update
    public void PlaySFX(string state)
    {
        switch (state)
        {
            case "move":
                if (!_move.isPlaying)
                {
                    _move.enabled = true;
                    _move.pitch = 1f;
                }
                _move.enabled = true;
                break;

            case "attack":
                sfx.PlayOneShot(_attack);
                break;

            case "attack2":
                sfx.PlayOneShot(_attack2);
                Invoke(nameof(Attack2), 0.3f);
                break;

            case "die":
                sfx.PlayOneShot(_die);
                break;

            case "hit":
                sfx.PlayOneShot(_hit);
                break;

            case "roar":
                sfx.PlayOneShot(_roar);
                break;

            case "idle":
                sfx.PlayOneShot(_idleRoad);
                break;

            case "stomMove":
                if (_move.isPlaying)
                    _move.enabled = false;
                break;

            case "roar2":
                sfx.PlayOneShot(_roar2);
                break;
        }
    }

    private void Attack2()
    {
        sfx.PlayOneShot(_attack2);
    }
}
