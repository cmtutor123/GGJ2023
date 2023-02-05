using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float invincibilityCooldown = 0;
    public float invincibilityTime;
    private int currentHealth;
    public int maxHealth;

    public bool FullHealth()
    {
        return currentHealth == maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        GameObject.Find("HealthBar").GetComponent<HealthBarManager>().SetHealthPercentage((float)currentHealth / maxHealth);
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        SceneManager.LoadScene("LoseScreen");
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
            if (currentHealth > maxHealth) currentHealth = maxHealth;
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
