using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private GameObject itemDisplay;
    private GameObject currentSlot;
    public ItemSlot currentItem;
    public ItemData itemData;

    private InventoryMenu viewController;

    private Image spawnedItemSprite;

    //Will link the item name to the name section in the actual inventory slot
    [SerializeField] private TMP_Text itemNameText;

    [SerializeField] private TMP_Text MaxAmmoText;
    [SerializeField] private TMP_Text CurrentAmmoText;
    // Start is called before the first frame update
    void Start()
    {
        itemDisplay.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        //Gets which slot is selected from the event system
        currentSlot = EventSystem.current.currentSelectedGameObject;
        //Sets up the actual item slot
            currentItem = currentSlot.GetComponent<ItemSlot>();
            //Gets the item data from the item slot
            itemData = currentItem.itemData;
            Debug.Log("TRUE OR FALSE: " +  currentItem.equipped);
            if (currentItem.equipped == true)
            {
                Debug.Log("PLEASE DISPLAY");
                itemDisplay.SetActive(true);  
                //Display(itemData);
            }
            else
            {
                //itemDisplay.SetActive(false);  
            }
    }
    private void Display(ItemData itemData)
    {
        if (itemData == null)
        {
            itemNameText.ClearMesh();
            return;
        } 
        if (spawnedItemSprite == null)
        {
            spawnedItemSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, transform);
        }
        MaxAmmoText.SetText(itemData.maxAmmo.ToString());
        CurrentAmmoText.SetText(itemData.currentAmmo.ToString());
    }
}
