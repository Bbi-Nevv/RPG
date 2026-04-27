using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum TypesOfEnemy
{
    Warrior,
    Archer,
}
public class EnemyController : MonoBehaviour
{
    private IState currentState;
    public TypesOfEnemy TypesOfEnemy;

    [Header("Attack Settings")]
    public float attackRange;
    public int damage = 1;
    public float attackCooldown = 1f;
    public bool isAttacking = false;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float chaseRange = 5f;
    public float faceDirection = -1f;
    public float distanceToPlayer;

    [Header("References")]
    public Transform player;
    public Rigidbody2D rb;
    public Animator anim;

    public bool isPlayerInRange = false;
    public Health_Enemy Health_Enemy;

    /// <summary>
    /// State instances for the enemy's behavior. 
    /// These states will be initialized in the Awake method and can be switched between using 
    /// the ChangeState method. Each state will have its own logic for entering, executing, and exiting, 
    /// allowing for a modular and organized approach to controlling the enemy's actions.
    /// Add new states here when you want to expand the enemy's behavior.
    /// </summary>
    public AttackState attackState;
    public RunState runState;
    public IdleState idleState;
    public StunState stunState;

    void Awake()
    {
        attackState = new AttackState(this);
        runState = new RunState(this);
        idleState = new IdleState(this);
        stunState = new StunState(this);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Health_Enemy = GetComponent<Health_Enemy>();
        
        ChangeState(idleState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
        TakeVisitPlayer();
        CaculateDistanceToPlayer();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }
    public float CaculateDistanceToPlayer()
    {
        if (player != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            return distanceToPlayer;
        }
        return 0f;
    }

    public Transform TakeVisitPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, chaseRange, LayerMask.GetMask("Player"));
        
        if (playerCollider != null)
        {
            player = playerCollider.transform;
            isPlayerInRange = true;
        }
        else
        {
            player = player = GameObject.FindGameObjectWithTag("Player").transform;
            isPlayerInRange = false;
        }
        return player;
    }
    public bool IsPlayerInRange()
    {
        return distanceToPlayer <= attackRange;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * faceDirection);
    }

    public void StartAttack()
    {
        if (IsPlayerInRange())
        {
            EventHub.TriggerHealthChange(-damage);
        }
    }

}
