using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyCombat))]
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



    [SerializeField] private AudioSource _moveSfx = null;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        respawPoint = transform.position;
        player = GameObject.FindFirstObjectByType<PlayerHealth>().transform;
        anim = GetComponent<Animator>();
        enemyCombat = GetComponent<EnemyCombat>();
        enemyHealth = GetComponent<EnemyHealth>();


    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        IsInResPawn();
        if (!playerInSightRange && !enemyCombat.playerInAttackRange && !enemyCombat.playerInAttackLongRange && inRespawnPosition && enemyHealth.isAlive) iddle();
        else if (!playerInSightRange && !enemyCombat.playerInAttackRange && !enemyCombat.playerInAttackLongRange && enemyHealth.isAlive && canMove && !stop) goToRespawn();
        if (playerInSightRange && !enemyCombat.playerInAttackRange && enemyHealth.isAlive && canMove && !stop) ChasePlayer();
    }

    private void IsInResPawn()
    {
        if (transform.position.x == respawPoint.x && transform.position.z == respawPoint.z) inRespawnPosition = true;
        else inRespawnPosition = false;
    }
    private void iddle()
    {
        anim.SetFloat("isMoving", 0f);
        //_moveSfx.enabled = false; 
    }
    private void goToRespawn()
    {
        //_moveSfx.enabled = true;
        agent.SetDestination(respawPoint);
        anim.SetFloat("isMoving", 0.5f);
    }
     private void ChasePlayer()
    {
        //_moveSfx.enabled = true;
        agent.SetDestination(player.position);
        anim.SetFloat("isMoving", 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}
