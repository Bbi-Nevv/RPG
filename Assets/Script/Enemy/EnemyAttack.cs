using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;
    public GameObject attackPoint;
    private float attackRange;
    public bool isAttacking = false;
    private Animator anim;
    private EnemyMovement enemyMovement;

    void Start()
    {
        anim = GetComponent<Animator>();
        attackPoint.SetActive(false);
        enemyMovement = GetComponent<EnemyMovement>();
    }
    public void Attack()
    {
        attackPoint.SetActive(true);
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, enemyMovement.detectionRange, enemyMovement.playerLayer);
        if (playerCollider != null)
        {
             //PlayerHeath playerHealth = playerCollider.GetComponent<PlayerHeath>();
             //if (playerHealth != null)
             //{
             //   playerHealth.TakeDamage(-damage);
             //}
             EventHub.TriggerHealthChange(-damage);
        }
    }

    public void EndAttack()
    {
        attackPoint.SetActive(false);
        isAttacking = false;
    }

    public float _attackRange()
    {
        attackRange = Vector2.Distance(transform.position, attackPoint.transform.position);
        return attackRange;
    }
}
