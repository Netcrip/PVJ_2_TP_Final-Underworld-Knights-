using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVolume : MonoBehaviour
{
    [SerializeField] VolumeData volumeData;
    [SerializeField] private Slider master,music,sfx;
    bool load= false;
    private void Awake()
    {
        volumeData= AudioManager.Instance.volumeData;
        master.value = volumeData.master;
        music.value = volumeData.music;
        sfx.value = volumeData.sfx;
        load = true;
    }

    public void VolumeChange()
    {
        if (load)
        {
            volumeData.master = master.value;
            volumeData.music = music.value;
            volumeData.sfx = sfx.value;
            AudioManager.Instance.ChangeAudio(volumeData);
        }
       
    }

}
