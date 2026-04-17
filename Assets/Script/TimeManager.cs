using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public EnemyController enemyController;
    private float cooldownTime;

    void Start()
    {
        cooldownTime = enemyController.attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();
    }

    public void Cooldown()
    {
        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
        }
    }
}
