using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public float cooldown = 2f;
    public GameObject attackPoint;

    private float timer;
    public bool isAttacking = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        attackPoint.SetActive(false);
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.J) && timer <=0)
        {
            animator.SetBool("isAttack", true);
        }
        
    }
    public void Attack()
    {
        attackPoint.SetActive(true);
        timer = cooldown;
        isAttacking = true;
    }

    public void EndAttack()
    {
        animator.SetBool("isAttack", false);
        attackPoint.SetActive(false);
        isAttacking = false;
    }

}