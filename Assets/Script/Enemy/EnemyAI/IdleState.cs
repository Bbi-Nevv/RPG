using UnityEngine;


public class IdleState : IState
{
    private EnemyController enemy;
    public IdleState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        enemy.anim.SetBool("isChase", false);
        enemy.anim.SetBool("isAttack", false);
        enemy.anim.Play("Idle");

        enemy.rb.linearVelocity = Vector2.zero;
    }
    public void Execute()
    {
        enemy.rb.linearVelocity = Vector2.zero;
        if (enemy.player != null)
        {
            if (enemy.IsPlayerInRange())
            {
                enemy.ChangeState(enemy.attackState);
            }

            if (!enemy.IsPlayerInRange() && enemy.distanceToPlayer <= enemy.chaseRange)
            {
                enemy.ChangeState(enemy.runState);
            }
        }
    }
    public void Exit() 
    {   
    }
}
