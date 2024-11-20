using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance {get; private set;}
     public string avatarSelection {get; private set;}=null;

    public PlayerHealth HealthInstance => healthInstance;
    private PlayerHealth healthInstance;

     public float playerHealth {get;private set;} 
     public float playerMaxHealth {get;private set;}

    [SerializeField] private float levelHealth;


    private void Awake(){
        if(Instance != null && Instance!= this){
            if(avatarSelection==null)
                playerHealth=0;
                avatarSelection="KnightB";

            Destroy(this);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

      public void SetAvatar(string selection){
        avatarSelection = selection;
    }

    public void PlayerHealth(PlayerHealth Health)
    {
        healthInstance = Health;
        healthInstance.onHealthchange += SetHealth;
        healthInstance.onDead += OnPlayerDead;
        healthInstance.onUnsuscribe += DoOnUnsuscribe;
        if(playerHealth<=0)
            playerHealth=levelHealth;
        else
            levelHealth=playerHealth;
    }

    private void SetHealth(float health,float maxHealt){
        playerHealth=health;
        playerMaxHealth=maxHealt;
        if(levelHealth==0)
            levelHealth=health;
        
    }
    private void DoOnUnsuscribe(){
        healthInstance.onHealthchange -= SetHealth;
        healthInstance.onDead -= OnPlayerDead;
        healthInstance.onUnsuscribe -= DoOnUnsuscribe;
    }

    public void InitialHealth(float health, float maxHealth){
        playerHealth = health;
        playerMaxHealth= maxHealth;
    }

    private void OnPlayerDead(){
        healthInstance.onHealthchange -= SetHealth;
        healthInstance.onDead -= OnPlayerDead;
        healthInstance.onUnsuscribe -= DoOnUnsuscribe;
        playerHealth=levelHealth;
        GameManager.Instance.LoadScene("Defeat",false);
    }

    

    
}
