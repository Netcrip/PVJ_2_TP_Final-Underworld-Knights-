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
    [SerializeField] float _maxHealt=100;
    private Combat combat;
    private PlayerInput playerInput;
    private Animator anim;
    //Eventos
    public Action onDead;
    public Action onUnsuscribe;
    public Action<float, float> onHealthchange;

    void Start()
    {
        UiManager.Instance.PlayerHealth(this);
        combat = GetComponent<Combat>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        health =_maxHealt;
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
            health -= (damageAmount/2);
            onHealthchange?.Invoke(health,_maxHealt);
        }
        else
        {
            anim.SetTrigger("Damage");
            health -= damageAmount;
            onHealthchange?.Invoke(health, _maxHealt);
        }
            

        if(health < 0)
        {
            onDead?.Invoke();
            anim.SetTrigger("Die");
            health = 0;
        }
    }

    public void Heal(float healAmount)
    {         
        if (health + healAmount <= _maxHealt)
        {
            health += healAmount;
            onHealthchange?.Invoke(health, _maxHealt);
        }
        else
        {
            health = _maxHealt;
            onHealthchange?.Invoke(health, _maxHealt);
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
