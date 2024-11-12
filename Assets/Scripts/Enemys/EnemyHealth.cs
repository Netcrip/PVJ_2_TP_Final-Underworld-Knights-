using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    //health
    private float health;
    [SerializeField] private float _maxHealth = 300;
    private HealthBarManager healthBar;
    
    private Animator anim;

    public bool isAlive { get; private set; }=true;

    void Awake()
    {
        health = _maxHealth;
        anim = GetComponent<Animator>();
        healthBar = GetComponent<HealthBarManager>();
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
            //_sfx.PlayOneShot(_hitSfx);
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
            Invoke(nameof(DestroyEnemy), 4f);
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
