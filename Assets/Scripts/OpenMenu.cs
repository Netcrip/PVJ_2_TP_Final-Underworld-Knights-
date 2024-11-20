using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
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
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
   
}
