using System.Collections;
using System.Data.SqlTypes;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AttackState : IState
{
    private EnemyController enemy;
    private float attackCooldown; 
    private float attackTimer;


    public AttackState(EnemyController enemy) 
    {
        this.enemy = enemy;
        attackCooldown = enemy.attackCooldown;
    }

    public void Enter()
    {
        //enemy.anim.SetBool("isAttack", true);

        attackTimer = Time.time;
    }
    public void Execute()
    {
        enemy.rb.linearVelocity = Vector2.zero;

        switch(enemy.TypesOfEnemy)
        {
            case TypesOfEnemy.Warrior:
                CheckAnimIsEnd("Attack1");
                if (Time.time >= attackTimer + attackCooldown)
                {
                    enemy.anim.SetBool("isAttack", true);
                }
                break;
            case TypesOfEnemy.Archer:
                CheckAnimIsEnd("Shoot");
                if (Time.time >= attackTimer + attackCooldown && enemy.isPlayerInRange)
                    enemy.anim.Play("Shoot");
                break;
        }
        Vector2 direction = (enemy.player.position - enemy.transform.position).normalized;
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
    public void Exit()
    {
        enemy.isAttacking = false;
        enemy.anim.SetBool("isAttack", false);
        Debug.Log("Exit Attack State");
    }

    public void CheckAnimIsEnd(string animName)
    {
        if (enemy.anim.GetCurrentAnimatorStateInfo(0).IsName(animName) &&
           (enemy.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f))
        {
            enemy.ChangeState(enemy.idleState);
        }
    }
}
