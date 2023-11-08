using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, ISelectHandler
{
    //Item equpping
    public bool equipped = false;
    public Transform player, playerMouth, itemSpriteHere;
    public Quaternion defaultRotation;
    public Rigidbody rb;
    public PlayerHealth health;


    //Item Data
    public ItemData itemData;

    //Reminder that this communicates with the greater inventory menu via InventoryMenu.cs
    private InventoryMenu viewController;

    private Image spawnedItemSprite;

    //Will link the item name to the name section in the actual inventory slot
    
    [SerializeField] private TMP_Text itemNameText;

    [SerializeField] private TMP_Text MaxAmmoText;
    [SerializeField] private TMP_Text divided;
    [SerializeField] private TMP_Text CurrentAmmoText;

    public void OnSelect(BaseEventData eventData)
    {
        if (itemData != null)
        {
            Debug.Log("Item Selected: " + itemData.ShortName);
        }

        viewController.OnSlotSelected(this);
        if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Fire1"))
        {
            EquipItem();
        }
    }

    public void EquipItem()
    {
            if (itemData != null)
            {            
                Debug.Log("Equipped Item: " + itemData.ShortName);
                if(itemData.ShortName == "SOCOM")
                {
                    // This gun is not yet equiped, so we need to create it
                    viewController.spawnedItem = Instantiate(itemData.itemModel, playerMouth, false);
                    viewController.equipedItem = itemData;
                    equipped = true;
                }


                //Handles non physical items (items with no view model)
                else 
                {
                    if(itemData.ShortName == "RATION")
                    {
                        viewController.equipedItem = itemData;
                        equipped = true;
                            // TODO: I don't think this implementation works
                            //  - Figure out some way to implement consumable rations
                        if (itemData.currentAmmo > 0 && health.currentHealth < 100)
                        {
                            health.HealHealth(100);
                            itemData.currentAmmo--;
                            if (itemData.currentAmmo == 0)
                            {
                                Destroy(spawnedItemSprite);
                                itemData = null;
                                RemoveItemData();
                            }
                        }
                    }
                    //We're not equiping a gun, so destroy it if equiped
                    if (viewController.spawnedItem != null) 
                    {
                        Destroy(viewController.spawnedItem);
                        viewController.spawnedItem = null;
                        viewController.equipedItem = null;
                    }
                    
                    if(itemData.ShortName == "NONE" || itemData.ShortName == null)
                    {
                        equipped = false;
                    }
                }
            }
            //Will unequip any item if you select a blank slot
            else if (itemData == null)
            {
                    viewController.equipedItem = null;
                    //We're not equiping a gun, so destroy it if equiped
                    if (viewController.spawnedItem != null) 
                    {
                        Destroy(viewController.spawnedItem);
                        viewController.spawnedItem = null;
                        viewController.equipedItem = null;
                    }
                    
                    if(itemData.ShortName == "NONE" || itemData.ShortName == null)
                    {
                        equipped = false;
                    }
            }
    }


    public void RemoveItemData()
    {
        itemNameText.SetText("NONE");
        MaxAmmoText.SetText("");
        CurrentAmmoText.SetText("");
        divided.SetText("");
    }
    public bool IsEmpty()
    {
        return itemData == null;
    }

    private void OnEnable()
    {
        if (itemData == null)
        {
            itemNameText.ClearMesh();
            return;
        } 
        if (spawnedItemSprite == null)
        {
            spawnedItemSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, itemSpriteHere);
        }
        MaxAmmoText.SetText(itemData.maxAmmo.ToString());
        CurrentAmmoText.SetText(itemData.currentAmmo.ToString());
        if (MaxAmmoText && CurrentAmmoText != null)
        {
            divided.SetText("/");
        }
        itemNameText.SetText(itemData.ShortName);
    }

    // private void OnDisable()
    // {
        
    // }

    private void Awake()
    {
        viewController = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>();

        //Handles our item slot. If there's no item, its null. Otherwise, we bring in the item icon
        if (itemData == null)
        {
            itemNameText.ClearMesh();
            return;
        }

        itemNameText.SetText(itemData.ShortName);
    }
}
