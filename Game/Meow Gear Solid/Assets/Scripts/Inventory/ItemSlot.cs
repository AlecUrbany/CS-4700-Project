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
    public PlayerHealth health;


    //Item Data
    public ItemData itemData;

    //Reminder that this communicates with the greater inventory menu via InventoryMenu.cs
    private InventoryMenu viewController;

    public RawImage equipmentIcon;

    //Will link the item name to the name section in the actual inventory slot
    public PlayerInventoryControls playerControls;
    [SerializeField] private TMP_Text itemNameText;

    [SerializeField] private TMP_Text MaxAmmoText;
    [SerializeField] private TMP_Text divided;
    [SerializeField] private TMP_Text CurrentAmmoText;

    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryControls>();
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (itemData != null)
        {
            Debug.Log("Item Selected: " + itemData.ShortName);
        }

        viewController.OnSlotSelected(this);
        if (Input.GetButton("Interact") || Input.GetButton("Fire1"))
        {
            playerControls.EquipItem(this);
        }
    }


    public void RemoveItemData()
    {
        itemData = null;
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
            itemNameText.SetText("NONE");
            equipmentIcon.texture = null;
            equipmentIcon.color = new Color(255,255,255, 0);
            return;
        } 
        else
        {
            equipmentIcon.texture = itemData.Sprite.mainTexture;
            equipmentIcon.color = new Color(255,255,255, 1);
            itemNameText.SetText(itemData.ShortName);
        }
    }
    private void Update()
    {
        if (MaxAmmoText == null || itemData == null)
        {
            return;
        }
        
        MaxAmmoText.SetText(itemData.maxAmmo.ToString());
        if (MaxAmmoText && CurrentAmmoText != null)
        {
            divided.SetText("/");
        }
        CurrentAmmoText.SetText(itemData.currentAmmo.ToString());
    }

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
