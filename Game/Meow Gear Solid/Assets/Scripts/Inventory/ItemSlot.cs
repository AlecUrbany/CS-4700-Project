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
    public Transform player, playerMouth;
    public Quaternion defaultRotation;
    public Rigidbody rb;
    public PlayerHealth health;


    //Item Data
    public ItemData itemData;

    private InventoryMenu viewController;

    private Image spawnedItemSprite;

    //Will link the item name to the name section in the actual inventory slot
    [SerializeField] private TMP_Text itemNameText;

    [SerializeField] private TMP_Text MaxAmmoText;
    [SerializeField] private TMP_Text CurrentAmmoText;

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Item Selected: " + itemData.ShortName);
        viewController.OnSlotSelected(this);
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Interact"))
        {
            EquipItem();
        }
    }

    public void EquipItem()
    {
            Debug.Log("Equipped Item: " + itemData.ShortName);

            if(itemData.ShortName == "SOCOM")
            {
                if(viewController.equipedItem != itemData)
                {
                    // This gun is not yet equiped, so we need to create it
                    viewController.spawnedItem = Instantiate(itemData.itemModel, playerMouth, false);
                    viewController.equipedItem = itemData;
                }
                equipped = true;

            } else 
            {
                // We're not equiping a gun, so destroy it if equiped
                if (viewController.spawnedItem != null) 
                {
                    Destroy(viewController.spawnedItem);
                    viewController.spawnedItem = null;
                    viewController.equipedItem = null;
                }

                if(itemData.ShortName == "RATION")
                {
                    // TODO: I don't think this implementation works
                    //  - Figure out some way to implement consumable rations
                    if (itemData.currentAmmo > 0) {
                        health.HealHealth(100);
                        itemData.currentAmmo--;
                    }
                    // Destroy(spawnedItemSprite);
                    // itemData = null;
                    equipped = false;
                }
                
                if(itemData.ShortName == "none")
                {
                    equipped = false;
                }
            }
    }
    
    // private void Update()
    // {
    //     if(equipped == true)
    //     {
    //         //itemData.itemModel.transform.position = playerMouth.position;
    //     }
    // }


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
        if (spawnedItemSprite == null) {
            spawnedItemSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, transform);
        }
        MaxAmmoText.SetText(itemData.maxAmmo.ToString());
        CurrentAmmoText.SetText(itemData.currentAmmo.ToString());
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
