using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{

    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip _clip;

    public void StartGame()
    {
        GameManager.Instance.LoadScene("Selecction",false);
    }
    public void GoMainMenu()
    {
        if(SceneManager.GetSceneByName("MainMenu").isLoaded)
            GameManager.Instance.UnloadSceneAdition("Creditos");
        else
            GameManager.Instance.LoadScene("MainMenu",false);
    }
    public void ReturnLevel()
    {
        GameManager.Instance.LoadScene(GameManager.Instance.lastLevel,true);
    }
    public void OpenAudioMenu()
    {
        GameManager.Instance.LoadSceneAdition("Audio");
        AudioManager.Instance.AudioOff();
    }

    public void OpenCreditsMenu()
    {
        GameManager.Instance.LoadSceneAdition("Creditos");
    }

    public void OpenExitMenu()
    {
        GameManager.Instance.LoadSceneAdition("ESCMenu");
        AudioManager.Instance.AudioOff();
    }
    public void BackMenu(string scene)
    {
        GameManager.Instance.UnloadSceneAdition(scene);
        //GameManager.Instance.LoadScene("MainMenu");
        AudioManager.Instance.AudioOn();
    }
    public void PlaySound(){
        _audio.PlayOneShot(_clip);
    }
}