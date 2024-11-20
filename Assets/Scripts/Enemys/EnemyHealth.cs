using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    //health
    private float health;
    [SerializeField] private float _maxHealth = 300;
    private HealthBarManager healthBar;
    
    private Animator anim;

    public bool isAlive { get; private set; }=true;

    private EnemyMove enemyMove;
    [SerializeField] float _stunDuration=5f;

    GameObject stunVfx;
    [SerializeField] GameObject Lightings;
    [SerializeField] GameObject LightingsTransform;

     public Action onDead;

    EnemySFX enemySFX;
    void Awake()
    {
        health = _maxHealth;
        anim = GetComponent<Animator>();
        healthBar = GetComponentInChildren<HealthBarManager>();
        enemyMove = GetComponent<EnemyMove>();
        GameManager.Instance.Finalboss(this);
        enemySFX = GetComponentInChildren<EnemySFX>();
    }

    // Update is called once per frame
    void Update()
    {
        IsAlive();
    }

    public void Damage(float damageAmount)
    {
        GetHit(damageAmount);
    }
    private void  GetHit(float damageAmount)
    {
        if (isAlive)
        {

            enemySFX.PlaySFX("hit");

            health -= damageAmount;
            healthBar.UpdateHealtBar(_maxHealth, health);

        }
    }

    private void IsAlive()
    {
        if (health > 0.0f && isAlive)
             isAlive = true;
        else
        {
            anim.SetBool("Die", true);
            isAlive = false;
            

            enemySFX.PlaySFX("die");

            Invoke(nameof(DestroyEnemy), 3f);
        }
    }
     public void StunOn(){
        enemyMove.stuned=true;
        stunVfx = Instantiate(Lightings, LightingsTransform.transform.position, Quaternion.identity);
        anim.SetBool("Stun",true);
        Invoke(nameof(StunOff), _stunDuration);

    }
    private void StunOff(){
        Destroy(stunVfx);    
        enemyMove.stuned=false;
        anim.SetBool("Stun",false);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        onDead?.Invoke();
    }

}
