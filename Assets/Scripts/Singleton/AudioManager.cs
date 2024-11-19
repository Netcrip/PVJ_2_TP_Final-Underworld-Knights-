using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public VolumeData volumeData { get; private set; }

    [SerializeField] AudioMixer audioMixer;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            //audioMixer = Resources.Load<AudioMixer>("A");
            GetVolume();
            DontDestroyOnLoad(gameObject);
        }

    }

    private void GetVolume()
    {
        volumeData = VolumeDataHandler.LoadVolumeData();
        SetVolum();
    }
    private void SaveVolume()
    {
        VolumeDataHandler.SaveVolumeData(volumeData);
    }
    private void SetVolum()
    {
        audioMixer.SetFloat("Master",Mathf.Log10(volumeData.master)*20);
        audioMixer.SetFloat("Music", Mathf.Log10(volumeData.music) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(volumeData.sfx) * 20);
    }
    public void ChangeAudio(VolumeData volumeData)
    {
        this.volumeData = volumeData;
        SaveVolume();
        SetVolum();
    }

}
