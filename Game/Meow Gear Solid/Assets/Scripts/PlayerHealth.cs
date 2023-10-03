using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvulnerable;
    public HealthBar healthBar;

    Color invisible;
    public Renderer bodyRender;

    public float MaxHealth{
        get { return MaxHealth; }
    }
    public float CurrentHealth{
        get { return CurrentHealth; }
        
    }
    void Start(){
        bodyRender = GetComponent<Renderer>();
        bodyRender.enabled = true;
        isInvulnerable = false;
        invisible = bodyRender.material.color;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
    public void TakeDamage(float damageAmount)
    {
        if(isInvulnerable == false)
        {
            StartCoroutine("GetInvulnerable");
            currentHealth -= damageAmount;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0){
                onDeath();
            }
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
        Debug.Log("GAME OVER");
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
    IEnumerator GetInvulnerable()
    {
        isInvulnerable = true;
        bodyRender.enabled = false;
        yield return new WaitForSeconds(.5f);
        bodyRender.enabled = true;
        yield return new WaitForSeconds(.5f);
        bodyRender.enabled = false;
        yield return new WaitForSeconds(.5f);
        bodyRender.enabled = true;
        yield return new WaitForSeconds(.5f);
        bodyRender.enabled = false;
        yield return new WaitForSeconds(.5f);
        bodyRender.enabled = true;
        yield return new WaitForSeconds(.5f);
        isInvulnerable = false;
    }
}
