using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingFunction : MonoBehaviour
{

    [SerializeField] private ItemData healData;

    public PlayerHealth playerHealth;
    public PlayerInventoryControls inventoryControls;
    public float healthRestored = 50;
    void Start()
    {
        healData = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>().equipedItem;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        inventoryControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryControls>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (healData.currentAmmo > 0 && playerHealth.currentHealth < 100)
            {
                playerHealth.HealHealth(healthRestored);
                healData.currentAmmo --;
                if (healData.currentAmmo == 0)
                {
                    healData = null;
                    inventoryControls.itemGone = true;
                }
            }
            
        }
    }
}
