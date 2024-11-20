using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance {get; private set;}

    public EnemyHealth HealthInstance => healthInstance;
    private EnemyHealth healthInstance;
    public string lastLevel {get; private set;}
   
    
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

    public void LoadScene(string scene, bool level){
        if(level){
            lastLevel=scene;

        }
        SceneManager.LoadScene(scene);
    }
    public void LoadSceneAdition(string scene){       
        
        if (!SceneManager.GetSceneByName(scene).isLoaded && !SceneManager.GetSceneByName("Audio").isLoaded)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);

            Time.timeScale = 0f;
        }
    }
    public void UnloadSceneAdition(string scene)
    {
        if (SceneManager.GetSceneByName(scene).isLoaded)
        {
            SceneManager.UnloadSceneAsync(scene);
            Time.timeScale = 1f; // Reanuda el tiempo si estaba pausado
        }
        
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
        LoadScene("Victory",false);
    }
}
