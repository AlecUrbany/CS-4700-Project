using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerInventoryControls : MonoBehaviour
{
    public Animator playerAnimator;
    [SerializeField] private ItemData defaultWeapon;
    public bool equipped;
    public bool wearingBox;
    public bool holdingGun;

    public bool itemGone;

    public bool hasBullets;
    public ItemData itemData;

    //Reminder that this communicates with the greater inventory menu via InventoryMenu.cs
    private InventoryMenu viewController;
    private GameObject intentoryMenu;

    public PlayerInventoryControls playerControls;
    public PlayerHealth health;

    public Transform player, playerHand, playerHead, itemSpriteHere;
    [SerializeField] private GameObject itemDisplay;
    [SerializeField] private TMP_Text itemNameText;
    public RawImage dispalyIcon;
    public RawImage bulletIcon;
    public RawImage newBullet;
    public GridLayoutGroup bulletGrid;
    public List<RawImage> magazineCount = new List<RawImage>();

    [SerializeField] private TMP_Text MaxAmmoText;
    [SerializeField] private TMP_Text divided;
    [SerializeField] private TMP_Text CurrentAmmoText;
    // Start is called before the first frame update
    void Start()
    {
        itemDisplay.SetActive(false);
        intentoryMenu = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>().inventoryViewObject;
        playerAnimator = GetComponentInChildren<Animator>();
        viewController.spawnedItem = Instantiate(defaultWeapon.itemModel, playerHand, false);
    }

    // Update is called once per frame
    void Update()
    {
        itemData = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>().equipedItem;
        if(itemData != null)
        {
            itemData.inInventory = true;
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
            dispalyIcon.texture = itemData.Sprite.mainTexture;
            if((itemData.weaponType == WeaponType.Melee) || (itemData.weaponType == WeaponType.Wearable))
            {
            MaxAmmoText.SetText("");
            CurrentAmmoText.SetText("");
            divided.SetText("");
            }
            dispalyIcon.color = new Color(255,255,255, 1);

            if((hasBullets == false && magazineCount.Capacity > 0) || itemData == null)
            {
                for (var i = bulletGrid.transform.childCount - 1; i >= 0; i--)
                {
                    Object.Destroy(bulletGrid.transform.GetChild(i).gameObject);
                }
            }
            
        }
        if(intentoryMenu.activeSelf == true)
        {
            itemDisplay.SetActive(false);
        }
        
    }

    public void DecreaseMagazine()
    {
        if(bulletGrid.transform.childCount > 0)
        {       
            Debug.Log("Removing bullet");
            var i = bulletGrid.transform.childCount - 1;
            Object.Destroy(bulletGrid.transform.GetChild(i).gameObject);
        }
    }
    public void ReloadMagazine()
    {
        if(bulletGrid.transform.childCount != itemData.magazine)
        {        
            for (var i = bulletGrid.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(bulletGrid.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < itemData.magazine; i++)
            {
                Debug.Log("Adding bullet");
                newBullet = Instantiate(bulletIcon, bulletGrid.transform, false);
                magazineCount.Add(newBullet);    
            }           
        }
    }
    public void DisplayItem(ItemData itemData, bool hasBullets)
    {
        if(itemData != null)
        {
            itemDisplay.SetActive(true);
            if(hasBullets == true)
            {
                itemNameText.SetText("");
                for (int i = 0; i < itemData.magazine; i++)
                {
                    Debug.Log("Adding bullet");
                    newBullet = Instantiate(bulletIcon, bulletGrid.transform, false);
                    magazineCount.Add(newBullet);    
                }   
            }
            else
            {
                Debug.Log("setting item name");
                itemNameText.SetText(itemData.ShortName);
            }
        }


        else
        {
            itemDisplay.SetActive(false);
        }
    }
    public void EquipItem(ItemSlot item)
    {
        
        
            if (item != null)
            {
                ItemData itemData = item.itemData;
                Debug.Log("Equipped Item: " + itemData.ShortName);
                wearingBox = false;
                playerAnimator.SetBool("WearingBox", wearingBox);
                holdingGun = false;
                playerAnimator.SetBool("HoldingGun", holdingGun);

                if((itemData.weaponType == WeaponType.Pistol) || (itemData.weaponType == WeaponType.Tranquilizer))
                {
                    Destroy(viewController.spawnedItem);
                    // This gun is not yet equiped, so we need to create it
                    viewController.spawnedItem = Instantiate(itemData.itemModel, playerHand, false);
                    viewController.equipedItem = itemData;
                    equipped = true;
                    hasBullets = true;
                    DisplayItem(itemData, hasBullets);
                    holdingGun = true;
                    playerAnimator.SetBool("HoldingGun", holdingGun);
                }

                if(itemData.weaponType == WeaponType.Wearable)
                {
                    Destroy(viewController.spawnedItem);
                    // This gun is not yet equiped, so we need to create it
                    viewController.spawnedItem = Instantiate(itemData.itemModel, playerHead, false);
                    viewController.equipedItem = itemData;
                    equipped = true;
                    hasBullets = false;
                    DisplayItem(itemData, hasBullets);
                    wearingBox = true;
                    playerAnimator.SetBool("WearingBox", wearingBox);
                }

                if(itemData.weaponType == WeaponType.Melee)
                {
                    Destroy(viewController.spawnedItem);
                    // This gun is not yet equiped, so we need to create it
                    viewController.spawnedItem = Instantiate(itemData.itemModel, playerHead, false);
                    viewController.equipedItem = itemData;
                    equipped = true;
                    hasBullets = false;
                    DisplayItem(itemData, hasBullets);
                }
                
                if(itemData.weaponType == WeaponType.Healing)
                {
                    Destroy(viewController.spawnedItem);
                    viewController.spawnedItem = Instantiate(itemData.itemModel, playerHand, false);
                    viewController.spawnedItem.GetComponent<healingFunction>().itemSlot = item;
                    viewController.equipedItem = itemData;
                    equipped = true;
                    hasBullets = false;
                    DisplayItem(itemData, hasBullets);

                }
               
            }

            else
            {
                    viewController.equipedItem = null;
                    //We're not equiping a gun, so destroy it if equiped
                    if (viewController.spawnedItem != null) 
                    {
                        Destroy(viewController.spawnedItem);
                        viewController.spawnedItem = null;
                        viewController.equipedItem = null;
                        if(bulletGrid.transform.childCount > 0)
                        {
                            for (var i = bulletGrid.transform.childCount - 1; i >= 0; i--)
                            {
                                Object.Destroy(bulletGrid.transform.GetChild(i).gameObject);
                            }                            
                        }
                        itemDisplay.SetActive(false);
                    }
                    equipped = false;
                    wearingBox = false;
                    playerAnimator.SetBool("WearingBox", wearingBox);
                    holdingGun = false;
                    playerAnimator.SetBool("HoldingGun", holdingGun);
            }
    }
    public void UnEquipItem()
    {
                    viewController.equipedItem = null;
                    //We're not equiping a gun, so destroy it if equiped
                    if (viewController.spawnedItem != null) 
                    {
                        Destroy(viewController.spawnedItem);
                        viewController.spawnedItem = null;
                        viewController.equipedItem = null;
                        if(bulletGrid.transform.childCount > 0)
                        {
                            for (var i = bulletGrid.transform.childCount - 1; i >= 0; i--)
                            {
                                Object.Destroy(bulletGrid.transform.GetChild(i).gameObject);
                            }                            
                        }
                        itemDisplay.SetActive(false);
                    }
                    equipped = false;
                    wearingBox = false;
                    playerAnimator.SetBool("WearingBox", wearingBox);
                    holdingGun = false;
                    playerAnimator.SetBool("HoldingGun", holdingGun);
                    viewController.spawnedItem = Instantiate(defaultWeapon.itemModel, playerHand, false);
            
    }
    private void Awake()
    {
        viewController = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>();

    }    
}