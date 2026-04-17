using UnityEngine;

public class RunState : IState
{
    /// <summary>
    /// this variable is used to store the reference of the EnemyController, 
    /// which is the main script that controls the enemy's behavior. 
    /// We need this reference to access the enemy's properties and methods, such as movement speed, 
    /// player position, and animation control.
    /// </summary>
    private EnemyController enemy; 

    public RunState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.anim.SetBool("isChase", true);
        enemy.anim.Play("Run"); 
    }
    public void Execute()
    {
        if (enemy.IsPlayerInRange())
        {
            enemy.ChangeState(enemy.attackState);
        }

        if (enemy.distanceToPlayer > enemy.chaseRange)
        {
            enemy.ChangeState(enemy.idleState);
        }

        if (enemy.player != null)
        {
            Vector2 direction = (enemy.player.position - enemy.transform.position).normalized;
            enemy.rb.linearVelocity = direction * enemy.moveSpeed;
            // Flip the enemy sprite based on the player's position
            if (direction.x > 0)
            {
                enemy.faceDirection = 1f;
                enemy.transform.localScale = new Vector3(enemy.faceDirection, 1f, 1f);
                enemy.Health_Enemy.healthBar.transform.localScale = new Vector3(enemy.faceDirection, 1f, 1f);
            }
            else if (direction.x < 0)
            {
                enemy.faceDirection = -1f;
                enemy.transform.localScale = new Vector3(enemy.faceDirection, 1f, 1f);
                enemy.Health_Enemy.healthBar.transform.localScale = new Vector3(enemy.faceDirection, 1f, 1f);
            }
        }
        else
        {
            enemy.rb.linearVelocity = Vector2.zero;
        }

    }
    public void Exit()
    {
        enemy.anim.SetBool("isChase", false);
    }

}
