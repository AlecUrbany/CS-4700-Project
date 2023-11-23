using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private GameObject itemDisplay;
    public Transform itemPosition;
    private GameObject currentSlot;
    public ItemSlot currentItem;
    public ItemData itemData;

    private InventoryMenu viewController;
    public bool displayOpen;
    private Image spawnedItemSprite;

    //Will link the item name to the name section in the actual inventory slot
    [SerializeField] private TMP_Text itemNameText;

    [SerializeField] private TMP_Text MaxAmmoText;
    [SerializeField] private TMP_Text divided;
    [SerializeField] private TMP_Text CurrentAmmoText;
    // Start is called before the first frame update
    void Start()
    {
        itemDisplay.SetActive(false);
        displayOpen = false;    
    }

    // Update is called once per frame
    void Update()
    {
        //Gets which slot is selected from the event system
        currentSlot = EventSystem.current.currentSelectedGameObject;
        //Sets up the actual item slot
            currentItem = currentSlot.GetComponent<ItemSlot>();
            if (currentItem != null && currentItem.equipped == true && displayOpen == false)
            {
                itemDisplay.SetActive(true);  
                Instantiate(currentItem, transform.position, Quaternion.identity);
                //Gets the item data from the item slot
                Display(currentItem.itemData);
                displayOpen = true;
            }
            else
            {
                itemDisplay.SetActive(false);
                Destroy(currentItem);
            }

            //Gets the item data from the item slot
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
        if (MaxAmmoText && CurrentAmmoText != null)
        {
            divided.SetText("/");
        }
        itemNameText.SetText(itemData.ShortName);
    }
    
}
