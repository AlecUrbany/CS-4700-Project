using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquippedSlot : MonoBehaviour
{
    //Item equpping
    public bool equipped = false;
    public Quaternion defaultRotation;
    public PlayerHealth health;


    //Item Data
    public ItemData itemData;

    public ItemSlot equippedItem;
    private InventoryMenu viewController;

    private Image spawnedItemSprite;

    //Will link the item name to the name section in the actual inventory slot
    [SerializeField] private TMP_Text itemNameText;

    [SerializeField] private TMP_Text MaxAmmoText;
    [SerializeField] private TMP_Text CurrentAmmoText;

    public void EquipItem(ItemData itemData)
    {
    }
    private void Update()
    {

    }


    public bool IsEmpty()
    {
        return itemData == null;
    }

    private void OnEnable()
    {
        viewController = FindObjectOfType<InventoryMenu>();
        if (itemData == null)
        {
            return;
        } 

        var spawnedSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, transform);
        MaxAmmoText.SetText(itemData.maxAmmo.ToString());
        CurrentAmmoText.SetText(itemData.currentAmmo.ToString());
    }
    private void OnDisable()
    {
        if (spawnedItemSprite != null)
        {
            Destroy(spawnedItemSprite);
        }
    }



    private void Awake()
    {
        viewController = FindObjectOfType<InventoryMenu>();

        //Handles our item slot. If there's no item, its null. Otherwise, we bring in the item icon
        if (itemData == null)
        {
            return;
        }

        var spawnedItemSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, transform);

        if (itemData == null)
        {
            itemNameText.ClearMesh();
            return;
        }
        itemNameText.SetText(itemData.ShortName);
        
    }
}
