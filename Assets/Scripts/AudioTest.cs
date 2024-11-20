using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioTest : MonoBehaviour
{
    [SerializeField] AudioClip _audioSFX;
    [SerializeField] AudioClip _audioMusic;
    [SerializeField] AudioClip _audioMaster;

    [SerializeField] Slider _master;
    [SerializeField] Slider _music;
    [SerializeField] Slider _sfx; 

    [SerializeField] AudioSource _audio;

    // Update is called once per frame
    private string selectedSlice;

    
    private void Start() {
        StopAudio();
        selectedSlice=null;
    }
    public void TestAudio(string seleceted){
        if(selectedSlice==seleceted){
            switch(seleceted){
            case "Master":
            AudioManager.Instance.SetAudioSystem(_master.value);
            break;
            case "Music":
            AudioManager.Instance.SetAudioSystem(_music.value);
            break;
            case "SFX":
            AudioManager.Instance.SetAudioSystem(_sfx.value);
            break;
            }
        }
        else{
            switch(seleceted){
            case "Master":
                Debug.Log("Entro");
                StopAudio(); 
                _audio.clip=_audioMaster;  
                _audio.Play();      
                selectedSlice=seleceted;
            AudioManager.Instance.SetAudioSystem(_master.value);
            break;
            case "Music":
                StopAudio(); 
                _audio.clip=_audioMusic;  
                _audio.Play();       
                selectedSlice=seleceted;
            AudioManager.Instance.SetAudioSystem(_music.value);
            break;
            case "SFX":  
                StopAudio();     
                _audio.clip=_audioSFX;
                _audio.Play();   
                selectedSlice=seleceted;
            AudioManager.Instance.SetAudioSystem(_sfx.value);
            break;

        }

        }   
    }

    private void StopAudio(){
        _audio.Stop(); 
    }
}
