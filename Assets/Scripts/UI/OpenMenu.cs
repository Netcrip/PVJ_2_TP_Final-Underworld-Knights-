using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip _clip;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameManager.Instance.LoadSceneAdition("ESCMenu");  
            AudioManager.Instance.AudioOff();
        }
              
    } 

    public void OpenAudioMenu()
    {
        GameManager.Instance.LoadSceneAdition("Audio");
        AudioManager.Instance.AudioOff();
    }
    public void CloseMenu(string scene)
    {
        GameManager.Instance.UnloadSceneAdition(scene);
        AudioManager.Instance.AudioOn();

        
    }
    public void PlaySound(){
        _audio.PlayOneShot(_clip);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
   
}
