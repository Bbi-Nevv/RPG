using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject prefab;

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerAttack playerAttack;
    public bool isStunned = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        EventHub.OnPlayerStun += Stun;
    }

    void Update()
    {
        if(!playerAttack.isAttacking)
        {
            Move();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

    }

    private void Move()
    {
        float ho = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        float ve = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;

        Vector2 movement = new Vector2(ho, ve).normalized;


        rb.linearVelocity = movement * moveSpeed;
        //transform.Translate(movement * moveSpeed * Time.deltaTime);

        animator.SetFloat("ho", Math.Abs(ho));
        animator.SetFloat("ve", Math.Abs(ve));

        if (ho > 0.1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (ho < -0.1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void SpawnStun()
    {
        Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
    }
    public void Stun(float time)
    {
        isStunned = true;
        rb.linearVelocity = Vector2.zero;
        SpawnStun();
        StartCoroutine(StunCoroutine(time));
        Destroy(prefab);
    }

    IEnumerator StunCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        isStunned = false;
    }
}
