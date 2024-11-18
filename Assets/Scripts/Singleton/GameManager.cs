using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance {get; private set;}

    public EnemyHealth HealthInstance => healthInstance;
    private EnemyHealth healthInstance;
   
    //public Vector2 _dungeonSize {get; private set;}
    public Vector3 respawn{get;private set;}
    private void Awake(){
        if(Instance != null && Instance!= this){
            Destroy(this);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
     

    public void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }
    public void LoadSceneAdition(string scene){
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    public void SetRespanw(Vector3 position){
        respawn = position;
    }
    public void Finalboss(EnemyHealth enemyHealth){
        healthInstance = enemyHealth;
        healthInstance.onDead+= DoOnDeadBoss;
    }

    private void DoOnDeadBoss(){
        healthInstance.onDead-= DoOnDeadBoss;
        LoadScene("Victory");
    }
}
