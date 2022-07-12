using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterStats characterStats;
   
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        characterStats = new CharacterStats(10, 10, 10, 10);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GlobalVar.playerDead = false;
        UIEventHandler.HealthChanged(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        //if (currentHealth <= 0)
        //{
        //    GlobalVar.playerDead = true;
        //}

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
            Die();
        
        UIEventHandler.HealthChanged(currentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player Dead. Reset Health.");
        currentHealth = maxHealth;
        UIEventHandler.HealthChanged(currentHealth, maxHealth);
    }
}
