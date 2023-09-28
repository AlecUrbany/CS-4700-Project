using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, ISelectHandler
{

    public ItemData itemData;

    private InventoryMenu viewController;

    private Image spawnedItemSprite;

    //Will link the item name to the name section in the actual inventory slot
    [SerializeField] private TMP_Text itemNameText;

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Item Selected");
        viewController.OnSlotSelected(this);
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
