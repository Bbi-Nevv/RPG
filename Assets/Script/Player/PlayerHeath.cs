using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeath : MonoBehaviour
{
    public float currentHealth = 3;
    public float maxHealth = 5;
    
    public Slider healthBar;

    void Start()
    {
        EventHub.OnHealthChange += TakeDamage;
        UpdateHeath();
    }
    public void TakeDamage(int amount)
    {
        currentHealth += amount;
        UpdateHeath();
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateHeath()
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
