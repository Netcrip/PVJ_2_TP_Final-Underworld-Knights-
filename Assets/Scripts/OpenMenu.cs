using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void OpenAudioMenu()
    {
        GameManager.Instance.LoadSceneAdition("Audio");
    }
    public void CloseMenu(string scene)
    {
        GameManager.Instance.UnloadSceneAdition(scene);
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
