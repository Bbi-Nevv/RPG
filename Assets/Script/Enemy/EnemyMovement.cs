using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;
    public EnemyState enemyState;
    public float detectionRange = 5f; 
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    private float faceDirection = -1f;
    private EnemyAttack enemyAttack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
        ChangeState(EnemyState.Idle);
    }
    void Update()
    {
        HandleDetection();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            if (enemyState == EnemyState.Chasing && !enemyAttack.isAttacking)
            {
                _Move();
            }
        }
    }

    private void HandleDetection()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (playerCollider != null)
        {
            player = playerCollider.transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= enemyAttack._attackRange())
            {
                enemyAttack.isAttacking = true;
                ChangeState(EnemyState.Attacking);
            }
            else if (!enemyAttack.isAttacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            if (player != null)
            {
                player = null;
                ChangeState(EnemyState.Idle);
            }
        }
    }

    public void _Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
        Flip();
    }
    void Flip()
    {
        if (player.position.x > transform.position.x && faceDirection > 0)
        {
            faceDirection *= -1;
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        else if (player.position.x < transform.position.x && faceDirection < 0)
        {
            faceDirection *= -1;
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
    }

    public void ChangeState(EnemyState State)
    {
        if (State == enemyState)
            return;

        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        enemyState = State;

        if (enemyState == EnemyState.Idle)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isAttacking", true);
        }
            
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (enemyAttack != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemyAttack._attackRange());
        }

        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}