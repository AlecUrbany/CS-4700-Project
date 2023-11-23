using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvulnerable;
    public Renderer player;
    public HealthBar healthBar;
    public VideoFader fader;
    [SerializeField] private GameObject GameOverScreen;

    public float MaxHealth{
        get { return MaxHealth; }
    }
    public float CurrentHealth{
        get { return CurrentHealth; }
        
    }
    void Start(){
        player = GetComponent<Renderer>();
        player.enabled = true;
        isInvulnerable = false;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        GameOverScreen.SetActive(false);

    }
    public void TakeDamage(float damageAmount)
    {
        // Debug.Log("TakeDamage(): " + isInvulnerable);
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

    public void onDeath()
    {
        float timer = 1f;
        GameOverScreen.SetActive(true);
        fader.FadeToBlack(timer);
        Debug.Log("GAME OVER");
        

    }

    void Update()
    {//tests our damage function. Must remove later
		if (Input.GetKeyDown(KeyCode.G))
		{
			TakeDamage(100);
		}
        if (Input.GetKeyDown(KeyCode.H))
		{
			HealHealth(20);
		}
    }
    IEnumerator GetInvulnerable()
    {
        isInvulnerable = true;
        StartCoroutine("FlashColor");
        yield return new WaitForSeconds(2f);
        StopCoroutine("FlashColor");
        isInvulnerable = false;
    }
    IEnumerator FlashColor()
    {
        Color invisible;
        invisible = player.material.color;
        int x = 0;
        while(x <= 10)
        {
            invisible.a = .25f;
            player.material.color = invisible;
            yield return new WaitForSeconds(.25f);
            invisible.a = 1f;
            player.material.color = invisible;
            yield return new WaitForSeconds(.25f);
            x++;
        }
    }

    
}
