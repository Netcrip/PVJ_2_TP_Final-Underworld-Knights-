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
        healthInstance.onDead += DoOnUnsuscribe;
        healthInstance.onUnsuscribe += DoOnUnsuscribe;
    }

    private void SetHealth(float healt,float maxHealt){
        playerHealth=healt;
        playerMaxHealth=maxHealt;
    }
    private void DoOnUnsuscribe(){
        healthInstance.onHealthchange -= SetHealth;
        healthInstance.onDead -= DoOnUnsuscribe;
        healthInstance.onUnsuscribe -= DoOnUnsuscribe;
    }

    public void InitialHealth(float health, float maxHealth){
        playerHealth = health;
        playerMaxHealth= maxHealth;
    }

    


    
}
