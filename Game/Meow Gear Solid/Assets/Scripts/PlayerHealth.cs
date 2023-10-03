using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;

    public float MaxHealth{
        get { return MaxHealth; }
    }
    public float CurrentHealth{
        get { return CurrentHealth; }
        
    }
    void Start(){
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
    public void TakeDamage(float damageAmount){
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0){
            onDeath();
        }
    }

    public void HealHealth(float healAmount){
        if(currentHealth + healAmount > maxHealth){
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
        else{
            currentHealth += healAmount;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void onDeath(){
        currentHealth = 200;
    }
    void Update()
    {//tests our damage function. Must remove later
		if (Input.GetKeyDown(KeyCode.G))
		{
			TakeDamage(20);
		}
        if (Input.GetKeyDown(KeyCode.H))
		{
			HealHealth(20);
		}
    }
}
