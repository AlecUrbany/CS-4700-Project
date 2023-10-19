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
        EquipItem(itemData.itemModel);
    }

    public void EquipItem(GameObject itemModel)
    {
        if(equipped != true)
        {
            if(itemData.ShortName == "SOCOM")
            {
                Debug.Log("Equipped Item: " + itemData.ShortName);
                equipped = true;
                GameObject newBullet = Instantiate(itemModel, playerMouth, false);
                /*itemData.itemModel.transform.position = playerMouth.position;
                itemData.itemModel.transform.localRotation = defaultRotation;*/
            }
            if(itemData.ShortName == "RATION")
            {
                Debug.Log("Equipped Item: " + itemData.ShortName);
                equipped = true;
                health.HealHealth(100);
                Destroy(itemData);
                equipped = false;
            }
        }
    }
    private void Update()
    {
        if(equipped == true)
        {
            //itemData.itemModel.transform.position = playerMouth.position;
        }
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
