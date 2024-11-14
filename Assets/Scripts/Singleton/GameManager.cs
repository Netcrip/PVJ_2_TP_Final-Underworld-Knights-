using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.WebCam;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance {get; private set;}
    public string avatarSelection {get; private set;}
    public Vector2 _dungeonSize {get; private set;}
    private void Awake(){
        if(Instance != null && Instance!= this){
            Destroy(this);
        }
        else{
            Instance = this;
            avatarSelection="KnightB";
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetAvatar(string selection){
        avatarSelection = selection;
    }   

    public void LoadScene(string scene){
        SceneManager.LoadScene(scene);
        AudioController.Instance.changeAudioSource(Camera.main.GetComponent<AudioSource>());
    }
    public void LoadSceneAdition(string scene){
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
}
