using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health_Enemy : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    public Slider healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        if(healthBar != null)
        {
            UpdateHealthUI();
        }
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        Debug.Log("Enemy took " + damage + " damage, current health: " + currentHealth);
    }
    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skill"))
        {
            TakeDamage(1);
            UpdateHealthUI();
        }
    }

}
