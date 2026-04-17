using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    private float lifeTime = 5f;
    private Vector2 movoment;
    private Archer archer;
    private Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        archer = FindAnyObjectByType<Archer>();
        archer.TakeVisitPlayer();
        direction = archer.player.transform.position - transform.position;
        movoment = direction.normalized;
        FlipArrow();

        rb.linearVelocity = movoment * speed;
        Destroy(gameObject, lifeTime);
    }

    void FlipArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventHub.TriggerHealthChange(-archer.damage);
            Destroy(gameObject);
        }

    }
}
