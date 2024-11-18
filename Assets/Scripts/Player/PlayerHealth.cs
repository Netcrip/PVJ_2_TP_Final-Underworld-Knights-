using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerHealth : MonoBehaviour, IDamagable
{
    // 
    [SerializeField]float health;
    [SerializeField] float _maxHealth=100;
    private Combat combat;
    private PlayerInput playerInput;
    private Animator anim;
    //Eventos
    public Action onDead;
    public Action onUnsuscribe;
    public Action<float, float> onHealthchange;

    PlayerSFX playerSFX;
    void Awake()
    {
        UiManager.Instance.PlayerHealth(this);
        PlayerManager.Instance.PlayerHealth(this);
        combat = GetComponent<Combat>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        if(PlayerManager.Instance.playerHealth==0){
            health = _maxHealth;
            onHealthchange?.Invoke(health, _maxHealth);
        }
        else{
            health=PlayerManager.Instance.playerHealth;
            _maxHealth=PlayerManager.Instance.playerMaxHealth;
            onHealthchange?.Invoke(health, _maxHealth);
        }
        playerSFX =GetComponent<PlayerSFX>();

    }
    private void Update()
    {
        Prueba();
    }
    // Update is called once per frame
    public void Damage(float damageAmount) { 
        GetHit(damageAmount);
    }

    private void GetHit(float damageAmount)
    {   
        if (combat.Defence)
        {
            anim.SetTrigger("Defence");
            playerSFX.playSFX("defence");
            health -= (damageAmount/2);
            onHealthchange?.Invoke(health,_maxHealth);
        }
        else
        {
            anim.SetTrigger("Damage");
            playerSFX.playSFX("hit");
            health -= damageAmount;
            onHealthchange?.Invoke(health, _maxHealth);
        }
            

        if(health < 0)
        {
            onDead?.Invoke();
            anim.SetTrigger("Die");
            playerSFX.playSFX("die");
            health = 0;
        }
    }

    public void Heal(float healAmount)
    {         
        if (health + healAmount <= _maxHealth)
        {
            health += healAmount;
            onHealthchange?.Invoke(health, _maxHealth);
        }
        else
        {
            health = _maxHealth;
            onHealthchange?.Invoke(health, _maxHealth);
        }
    }
    public void Unsuscribe(){
        onUnsuscribe?.Invoke();
    }

    private void Prueba()
    {
       
        if (playerInput.Damage)
        {
            Debug.Log("Damage");
            GetHit(15);
        }
        if (playerInput.Heal)
        {
            Debug.Log("Heal");
            Heal(10);
            
        }
    }
}
