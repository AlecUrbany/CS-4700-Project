using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{

    // Handles inventory basics - names, descriptions, etc.
    [SerializeField] public GameObject inventoryViewObject;
    [SerializeField] private GameObject inventoryTextBox;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescText;

    //Will handle player interactions
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private List<ItemSlot> slots;

    // Handles equiping and unequiping of the gun
    public ItemData equipedItem;
    public GameObject spawnedItem;

    //Makes sure inventory doesnt spawn in when we start the game
    void Start()
    {
        inventoryViewObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        //When the inventory button is pressed, time is stopped (so the player cant move and enemies cant attack)
        if (Input.GetButtonDown("Toggle Inventory"))
        {
            if(inventoryViewObject.activeSelf == true)
            {
                EventBus.Instance.OpenInventory();
                Time.timeScale = 1;
                
            }
            
            else
            {
                EventBus.Instance.CloseInventory();
                Time.timeScale = 0;    
            }
            inventoryViewObject.SetActive(!inventoryViewObject.activeSelf);
        }
                
    }

    private void OnEnable()
    {
        if(EventBus.Instance != null)
        {
            EventBus.Instance.onPickUpItem += OnItemPickedUp;
        }
        inventoryTextBox.SetActive(false);
    }
    private void OnDisable()
    {
        if(EventBus.Instance != null)
        {
            EventBus.Instance.onPickUpItem -= OnItemPickedUp;
        }
    }

    private void OnItemPickedUp(ItemData itemData)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.itemData = itemData;
                break;
            }
        }
    }

    public void OnSlotSelected(ItemSlot selectedSlot)
    {
        if (selectedSlot.itemData == null)
        {
            itemNameText.ClearMesh();
            itemDescText.ClearMesh();
            inventoryTextBox.SetActive(false);
            return;
        }
        //Set the item name
        itemNameText.SetText(selectedSlot.itemData.Name);
        //Set the item description
        itemDescText.SetText(selectedSlot.itemData.Description[0]);
        inventoryTextBox.SetActive(true);
    }
    
}
