using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float invincibilityCooldown = 0;
    public float invincibilityTime;
    private int currentHealth;
    public int maxHealth;

    void PlayerDeath()
    {
        Debug.Log("Player Died");
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (invincibilityCooldown < 0)
        {
            invincibilityCooldown = invincibilityTime;
            currentHealth -= damage;
            if (currentHealth < 0) currentHealth = 0;
            GameObject.Find("HealthBar").GetComponent<HealthBarManager>().SetHealthPercentage((float)currentHealth / maxHealth);
            if (currentHealth <= 0)
            {
                PlayerDeath();
            }
        }
    }

    void Update()
    {
        invincibilityCooldown -= Time.deltaTime;
    }
}
