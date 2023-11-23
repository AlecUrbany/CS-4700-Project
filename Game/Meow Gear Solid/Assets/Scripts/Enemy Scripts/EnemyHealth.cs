using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float MaxHealth{
        get { return MaxHealth; }
    }
    public float CurrentHealth{
        get { return CurrentHealth; }
    }
    void Start(){
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damageAmount){
        currentHealth -= damageAmount;
        if(currentHealth <= 0){
            onDeath();
        }
    }

    public void HealHealth(float healAmount){
        if(currentHealth + healAmount > maxHealth){
            currentHealth = maxHealth;
        }
        else{
            currentHealth += healAmount;
        }
    }

    public void onDeath(){
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
