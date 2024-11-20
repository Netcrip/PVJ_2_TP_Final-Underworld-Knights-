using UnityEngine;
public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.LoadScene("Selecction",false);
    }
    public void OpenAudioMenu()
    {
        GameManager.Instance.LoadSceneAdition("Audio");
    }

    public void OpenCreditsMenu()
    {
        GameManager.Instance.LoadSceneAdition("Creditos");
    }

    public void OpenExitMenu()
    {
        GameManager.Instance.LoadSceneAdition("ESCMenu");
    }
    public void BackMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }
}