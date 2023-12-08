using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingFunction : MonoBehaviour
{

    [SerializeField] private ItemData healData;

    public PlayerHealth playerHealth;
    public PlayerInventoryControls inventoryControls;
    public ItemSlot itemSlot;
    public float healthRestored = 50;
    void Start()
    {
        healData = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>().equipedItem;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        inventoryControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryControls>();
       // itemSlot = GameObject.FindGameObjectWithTag("HUD").GetComponent<ItemSlot>();
        
    }

    void Update()
    {
        if(EventBus.Instance.canMove == false)
        {
            return;
        }
        
        if(Input.GetButtonDown("Fire1"))
        {
            if (healData.currentAmmo > 0 && playerHealth.currentHealth < 100)
            {
                playerHealth.HealHealth(healthRestored);
                healData.currentAmmo --;
            } 
        }
        if(healData != null)
        {
            if (healData.currentAmmo == 0)
            {
                    healData.inInventory = false;
                    itemSlot.itemData = null;
                    inventoryControls.itemGone = true;
                    inventoryControls.EquipItem(null);
                    itemSlot.RemoveItemData();
            }
        }        
    }
}
