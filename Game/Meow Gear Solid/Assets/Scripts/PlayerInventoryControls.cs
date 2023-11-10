using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerInventoryControls : MonoBehaviour
{
    public bool equipped;
    public bool itemGone;

    public bool hasBullets;
    public ItemData itemData;

    //Reminder that this communicates with the greater inventory menu via InventoryMenu.cs
    private InventoryMenu viewController;

    public PlayerInventoryControls playerControls;
    public PlayerHealth health;

    public Transform player, playerMouth, itemSpriteHere;
    [SerializeField] private GameObject itemDisplay;
    [SerializeField] private TMP_Text itemNameText;
    public RawImage dispalyIcon;
    public RawImage bulletIcon;
    public Transform newBullet;
    public GridLayoutGroup bulletGrid;
    public List<Transform> magazineCount = new List<Transform>();

    [SerializeField] private TMP_Text MaxAmmoText;
    [SerializeField] private TMP_Text divided;
    [SerializeField] private TMP_Text CurrentAmmoText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(itemData != null)
        {
            itemDisplay.SetActive(true);
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
        
    }
    public void DisplayItem(ItemData itemData, bool hasBullets)
    {
            if(itemData != null)
            {
                itemDisplay.SetActive(true);
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
            dispalyIcon.texture = itemData.Sprite.mainTexture;
            dispalyIcon.color = new Color(255,255,255, 1);
            if(hasBullets == true)
            {
                itemNameText.SetText("");
                for (int i = 0; i < itemData.MagazineSize; i++)
                {
                    newBullet = Instantiate(bulletIcon.transform, bulletGrid.transform, false);
                    magazineCount.Add(newBullet);
                }
            }
            else
            {
                itemNameText.SetText(itemData.ShortName);

            }
    }
    public void EquipItem(ItemData itemData)
    {
            if (itemData != null)
            {
                Debug.Log("Equipped Item: " + itemData.ShortName);

                if((itemData.weaponType == WeaponType.Pistol) || (itemData.weaponType == WeaponType.Pistol))
                {
                    Destroy(viewController.spawnedItem);
                    // This gun is not yet equiped, so we need to create it
                    viewController.spawnedItem = Instantiate(itemData.itemModel, playerMouth, false);
                    viewController.equipedItem = itemData;
                    equipped = true;
                    hasBullets = true;
                    DisplayItem(itemData, hasBullets);

                }
                
                if(itemData.weaponType == WeaponType.Healing)
                {
                    Destroy(viewController.spawnedItem);
                    viewController.spawnedItem = Instantiate(itemData.itemModel, playerMouth, false);
                    viewController.equipedItem = itemData;
                    equipped = true;
                    if (itemGone == true)
                    {
                        itemData = null;
                        Destroy(viewController.spawnedItem);
                        itemGone = false;
                        equipped = false;
                    }

                }

                if(itemData.weaponType == WeaponType.Consumable)
                {

                }
               
            }
            else if (itemData == null)
            {
                    viewController.equipedItem = null;
                    //We're not equiping a gun, so destroy it if equiped
                    if (viewController.spawnedItem != null) 
                    {
                        Destroy(viewController.spawnedItem);
                        viewController.spawnedItem = null;
                        viewController.equipedItem = null;
                        itemDisplay.SetActive(false);
                    }
                    equipped = false;
            }
    }
    private void Awake()
    {
        viewController = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>();
    }    
}

