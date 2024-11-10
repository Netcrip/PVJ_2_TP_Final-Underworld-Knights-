using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerHealth : MonoBehaviour, IDamagable
{
    // 
    float health;
    [SerializeField] float _maxHealt=100;
    private Combat _combat;
    private PlayerInput _playerInput;

    //Eventos
    public Action onDead;
    public Action<float, float> onHealthchange;

    void Start()
    {
        UiManager.Instance.PlayerHealth(this);
        _combat = GetComponent<Combat>();
        _playerInput = GetComponent<PlayerInput>();
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
        if (_combat.Defence)
        {
            health -= (damageAmount/2);
            onHealthchange?.Invoke(health,_maxHealt);
        }
        else
        {
            health -= damageAmount;
            onHealthchange?.Invoke(health, _maxHealt);
        }
            

        if(health < 0)
        {
            onDead?.Invoke();
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

    private void Prueba()
    {
       
        if (_playerInput.Damage)
        {
            Debug.Log("Damage");
            GetHit(15);
        }
        if (_playerInput.Heal)
        {
            Debug.Log("Heal");
            Heal(10);
            
        }
    }
}
