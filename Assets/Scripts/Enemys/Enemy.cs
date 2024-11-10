using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    private NavMeshAgent agent;

    [SerializeField] private Transform player;

    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    //health
    private float health;
    [SerializeField]private float _maxHealth=300;
    [SerializeField] private HealtBarManager _healthBar;
    [SerializeField]private bool isAlive;
    
    // Animator
    [SerializeField] private Animator _anim;


    //Respawn point
    [SerializeField] private Vector3 _respawPoint;

    //Attacking
    [SerializeField] private float _timeBetweenAttacks, _timeBetweenlongAttacks;
    bool alreadyAttacked;
    [SerializeField] private GameObject _sword;
    [SerializeField] private Transform _swordPosition;
    private RaycastHit hit;
    [SerializeField] private float _damageAttack;
    [SerializeField] private GameObject _swordShow;
    private bool trhowRock = false;

    //
    [SerializeField] private bool canMove = true;
    private bool stop = false;

    //States
    [SerializeField] private float _sightRange, _attackRange, _attackLongRange, _rotationSpeed;
    private bool playerInSightRange, playerInAttackRange,playerInAttackLongRange,inRespawnPosition;

    //audio
    [SerializeField] private AudioSource _sfx=null;
    [SerializeField] private AudioSource _moveSfx=null;
    [SerializeField] private AudioClip _hitSfx=null;
    [SerializeField] private AudioClip _attackSfx=null;
    [SerializeField] private AudioClip _attack2Sfx=null;
    [SerializeField] private AudioClip _attack2SwordSfx=null;
    [SerializeField] private AudioClip _dieSfx=null;
    private bool isDead = false;

    //public Action <Boss>onDead;
    private void Awake()
    {
        //player = GameObject.Find("Player").transform;
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        _respawPoint = transform.position;
        health = _maxHealth;
        //GameMangarer.Instance.boosCreate(this);
        //PlayertUiManager.Instance.onPlayerDead += Stop;
    }

    private void Update()
    {
        //Comprobasion rango para seguir y atacar 
        playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, whatIsPlayer);
        playerInAttackLongRange = Physics.CheckSphere(transform.position, _attackLongRange-1f, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange-1f, whatIsPlayer);
        if(transform.position.x == _respawPoint.x && transform.position.z == _respawPoint.z) inRespawnPosition = true;            
        else inRespawnPosition = false;

        if ( health > 0.0f) isAlive=true; else isAlive=false;
        if (!playerInSightRange && !playerInAttackRange && !playerInAttackLongRange && inRespawnPosition && isAlive) iddle();
        else if (!playerInSightRange && !playerInAttackRange && !playerInAttackLongRange && isAlive && canMove && !stop) goToRespawn();
        if (playerInAttackLongRange && !playerInSightRange && isAlive && !stop) LongAttackPlayer();
        if (playerInSightRange && !playerInAttackRange && isAlive && canMove && !stop) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && isAlive && !stop) AttackPlayer();


    }
    private void FixedUpdate()
    {
        if (trhowRock)
        {
            RockThrower();
            trhowRock=false;
        }
    }
    private void iddle() 
    {
       // _anim.SetFloat("isMoving", 0f);
        //_moveSfx.enabled = false; 
    }
    
    private void goToRespawn()
    {
        //_moveSfx.enabled = true;
        agent.SetDestination(_respawPoint);
        //_anim.SetFloat("isMoving", 0.5f);
    }

    private void toggleCanmove()
    {
        canMove = !canMove;
    }

    private void ChasePlayer()
    {
        //_moveSfx.enabled = true;
        agent.SetDestination(player.position);
        //_anim.SetFloat("isMoving",0.5f);
    }

    private void LongAttackPlayer()
    {
        //fijar enemigo
        
        //_moveSfx.enabled = false;
        agent.SetDestination(transform.position);
        //_anim.SetFloat("isMoving", 0f);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            

            Ray ray = new Ray(transform.position, transform.forward);
            if(Physics.Raycast(ray,out hit, _attackLongRange))
            {
                canMove = false;
                //_sfx.PlayOneShot(_attack2SwordSfx);
                //_sfx.PlayOneShot(_attack2Sfx);
                //_anim.SetTrigger("longAttack");

                Invoke(nameof(ShowRock), 0.6f);
                ///Calculamos la disntacia para la fuerza de la piedra
               // sfx.PlayOneShot(attack2StoneSfx);
                Invoke(nameof(RockThrowerCall),1.6f);
                Invoke(nameof(toggleCanmove), 2f);
                alreadyAttacked = true;
             Invoke(nameof(ResetAttack), _timeBetweenlongAttacks);
            }
            
        }
        
    }

    //stone 
    private void ShowRock()
    {
        _swordShow.SetActive(true);
        
    }
    private void RockThrowerCall()
    {
        trhowRock = true;
    }

    private void RockThrower()
    {
        
        _swordShow.SetActive(false);
        Rigidbody rb = Instantiate(_sword, _swordPosition.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * (hit.distance / 1.6f), ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.Range(0,500), UnityEngine.Random.Range(0, 500), UnityEngine.Random.Range(0, 500));
    }

    private void AttackPlayer()
    {
        
        //fijar enemigo 
        //_moveSfx.enabled = false;
        agent.SetDestination(transform.position);
        //_anim.SetFloat("isMoving", 0f);
     
        if (!alreadyAttacked)
        {
            //mirar al jugador
            Quaternion newRotation = Quaternion.LookRotation( player.transform.position- transform.position);
            Quaternion currentRotation = transform.rotation;
            Quaternion finalRotation = Quaternion.Lerp(currentRotation, newRotation, Time.deltaTime * _rotationSpeed);
            transform.rotation = finalRotation;




            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, _attackRange))
            {
                canMove = false;
                //_sfx.PlayOneShot(_attackSfx);
                //_anim.SetTrigger("shortAttack");

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), _timeBetweenAttacks);
                Invoke(nameof(toggleCanmove), 2f);
            }

           
        }
       
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void Stop()
    {
        stop = true;
        //PlayertUiManager.Instance.onPlayerDead -= Stop;
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Lógica para aplicar daño al enemigo
        PlayerHealth isplayer = other.GetComponent<PlayerHealth>();
        if (isplayer != null)
        {
            isplayer.Damage(_damageAttack);
        }
    }
    public void Damage(float damage)
    {
        if (!isDead)
        {
            //_sfx.PlayOneShot(_hitSfx);
            health -= damage;
            _healthBar.UpdateHealtBar(_maxHealth, health);
        }
        if (health <= 0 && !isDead)
        {
            //_sfx.PlayOneShot(_dieSfx);
            //_anim.SetBool("isDead", true);
            //onDead?.Invoke(this);
            isDead = true;
            Invoke(nameof(DestroyEnemy), 4f);
            
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackLongRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}
