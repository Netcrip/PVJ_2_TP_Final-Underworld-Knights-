using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(EnemySFX))]
public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent agent;

    public  Transform player { get; private set; }

    [SerializeField] private float _sightRange, _rotationSpeed;
    public bool playerInSightRange { get; private set; }

    private Animator anim;

    private Vector3 respawPoint;

    public bool canMove { set; get; } = true;

    public bool stop { private set; get; }

    [SerializeField] private LayerMask _whatIsPlayer;

    private EnemyCombat enemyCombat;
    private EnemyHealth enemyHealth;

    private bool inRespawnPosition;

    public bool stuned{set;get;}



    EnemySFX enemySFX;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        respawPoint = transform.position;
        player = GameObject.FindFirstObjectByType<PlayerHealth>().transform;
        anim = GetComponent<Animator>();
        enemyCombat = GetComponent<EnemyCombat>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemySFX = GetComponent<EnemySFX>();

        
    }

    private void Start()
    {
        enemySFX.PlaySFX("roar");
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        IsInResPawn();
        if (!playerInSightRange && !enemyCombat.playerInAttackRange && !enemyCombat.playerInAttackLongRange && inRespawnPosition && enemyHealth.isAlive && !stuned) iddle();
        else if (!playerInSightRange && !enemyCombat.playerInAttackRange && !enemyCombat.playerInAttackLongRange && enemyHealth.isAlive && canMove && !stop && !stuned) goToRespawn();
        if (playerInSightRange && !enemyCombat.playerInAttackRange && enemyHealth.isAlive && canMove && !stop && !stuned) ChasePlayer();
    }

    private void IsInResPawn()
    {
        if (transform.position.x == respawPoint.x && transform.position.z == respawPoint.z) inRespawnPosition = true;
        else inRespawnPosition = false;
    }
    private void iddle()
    {
        anim.SetFloat("isMoving", 0f);
        enemySFX.PlaySFX("idle");
    }
    private void goToRespawn()
    {
        
        agent.SetDestination(respawPoint);
        anim.SetFloat("isMoving", 0.5f);
        enemySFX.PlaySFX("move");
    }
     private void ChasePlayer()
    {
        
        agent.SetDestination(player.position);
        anim.SetFloat("isMoving", 0.5f);
        enemySFX.PlaySFX("move");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}
