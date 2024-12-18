using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using System;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent (typeof(EnemyMove))]
public class EnemyCombat : MonoBehaviour
{
    public bool playerInAttackLongRange {  get; private set; }
    public bool playerInAttackRange { get; private set; }

    private EnemyHealth enemyHealth;
    private EnemyMove enemyMove;

    Animator anim;

    NavMeshAgent agent;

    //Attacking
    [SerializeField] private float _timeBetweenAttacks, _timeBetweenlongAttacks;
    bool alreadyAttacked;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _weaponPosition;
    private RaycastHit hit;
    [SerializeField] private float _damageAttack;
    private bool trhowWeapon = false;

    [SerializeField] private float  _attackRange, _attackLongRange, _rotationSpeed;

    [SerializeField] LayerMask _whatIsPlayer;

    [SerializeField] BoxCollider _weaponCollider;

    private bool spinAttack;

    EnemySFX enemySFX;

    // Start is called before the first frame update
    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMove = GetComponent<EnemyMove>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemySFX = GetComponent<EnemySFX>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackLongRange = Physics.CheckSphere(transform.position, _attackLongRange - 1f, _whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange - 1f, _whatIsPlayer);
        if (playerInAttackLongRange && !enemyMove.playerInSightRange && enemyHealth.isAlive && !enemyMove.stop && !enemyMove.stuned) LongAttackPlayer();
        if (playerInAttackRange && enemyMove.playerInSightRange && enemyHealth.isAlive && !enemyMove.stop && !enemyMove.stuned) AttackPlayer();
    }

    private void FixedUpdate()
    {
        if (trhowWeapon)
        {
            SwordThrower();
            trhowWeapon = false;
        }
    }
    private void LongAttackPlayer()
    {
        //fijar enemigo

        //_moveSfx.enabled = false;
        agent.SetDestination(transform.position);
        anim.SetFloat("isMoving", 0f);
        enemySFX.PlaySFX("stomMove");
        transform.LookAt(enemyMove.player);

        if (!alreadyAttacked)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 30f);
            if (Physics.Raycast(ray, out hit, _attackLongRange, _whatIsPlayer))
            {
                enemyMove.canMove = false;
             
                anim.SetTrigger("Attack2");
                enemySFX.PlaySFX("roar2");
 
                Invoke(nameof(SwordThrowerCall), 0.5f);
                Invoke(nameof(toggleCanmove), 1f);
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), _timeBetweenlongAttacks);
            }

        }

    }
    private void SwordThrower()
    {
        ///_swordShow.SetActive(false);
        Rigidbody rb = Instantiate(_weapon, _weaponPosition.position, Quaternion.Euler(0, 90, 0)).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * (hit.distance / 2f), ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        //rb.AddTorque(UnityEngine.Random.Range(0,500), UnityEngine.Random.Range(0, 500), UnityEngine.Random.Range(0, 500));
        rb.AddTorque(transform.up * 360f);
    }

    private void AttackPlayer()
    {

        //fijar enemigo 
        //_moveSfx.enabled = false;
        agent.SetDestination(transform.position);
        anim.SetFloat("isMoving", 0f);
        enemySFX.PlaySFX("stomMove");

        if (!alreadyAttacked)
        {
            //mirar al jugador
            Quaternion newRotation = Quaternion.LookRotation(enemyMove.player.transform.position - transform.position);
            Quaternion currentRotation = transform.rotation;
            Quaternion finalRotation = Quaternion.Lerp(currentRotation, newRotation, Time.deltaTime * _rotationSpeed);
            transform.rotation = finalRotation;

            if(UnityEngine.Random.Range(0,100)>90){
                enemyMove.canMove = false;
                _weaponCollider.enabled = true;
                //_sfx.PlayOneShot(_attackSfx);                
                anim.SetTrigger("SpinAttack");
                enemySFX.PlaySFX("attack2");
            
                alreadyAttacked = true;
                spinAttack=true;
                Invoke(nameof(ResetAttack), _timeBetweenAttacks+2f);
                Invoke(nameof(toggleCanmove), 1f);
            }
            else{
                if (IsFron())//Physics.Raycast(ray, out hit, _attackRange))
                {

                enemyMove.canMove = false;
                _weaponCollider.enabled = true;
                anim.SetTrigger("Attack1");
                enemySFX.PlaySFX("attack1");
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), _timeBetweenAttacks);
                Invoke(nameof(toggleCanmove), 1f);
                }
            }
            //Ray ray = new Ray(transform.position, transform.forward);
            

        }

    }



    bool IsFron()
    {
        Vector3 directionOfPlayer = transform.position - enemyMove.player.transform.position;
        float angle = Vector3.Angle(transform.forward, directionOfPlayer);
        if (Math.Abs(angle) > 170 && Math.Abs(angle) < 180)
        {
            return true;
        }
        else
            return false;
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        _weaponCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // L�gica para aplicar da�o al enemigo
        PlayerHealth isplayer = other.GetComponent<PlayerHealth>();
        if (isplayer != null)
        {
            if(!spinAttack)
                _weaponCollider.enabled = false;
            isplayer.Damage(_damageAttack);
        }
    }

    private void SwordThrowerCall()
    {
        trhowWeapon = true;
    }

    private void toggleCanmove()
    {
        enemyMove.canMove = !enemyMove.canMove;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackLongRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
