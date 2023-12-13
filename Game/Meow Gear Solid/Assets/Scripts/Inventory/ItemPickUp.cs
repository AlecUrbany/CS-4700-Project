using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextBox;
    private string itemNameText;

    public float nameLifeSpan = .5f;
    [SerializeField] private ItemData itemData;
    public AudioClip pickUpSound;
    void Start()
    {
        itemData.inInventory = false;
    }
    private void OnTriggerStay(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (Input.GetButton("Interact"))
        {
            if((itemData.weaponType == WeaponType.Pistol) || (itemData.weaponType == WeaponType.Tranquilizer))
            {
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position, 2f);
                Debug.Log("picked up " + itemData.ShortName);
                itemData.currentAmmo = itemData.maxAmmo;
                itemData.magazine = itemData.magazineSize;
                itemNameText = itemData.ShortName;
                ShowText(itemNameText);
                EventBus.Instance.PickUpItem(itemData);
                Destroy(gameObject);
            }

            if((itemData.weaponType == WeaponType.Healing) || (itemData.weaponType == WeaponType.Throwable) || (itemData.weaponType == WeaponType.Consumable))
            {
                Debug.Log("picked up " + itemData.ShortName);
                if(itemData.inInventory == true)
                {
                    if (itemData.currentAmmo < itemData.MaxAmmo)
                    {
                        AudioSource.PlayClipAtPoint(pickUpSound, transform.position, 2f);
                        itemData.currentAmmo += 1;
                        Destroy(gameObject);
                    }
                    else
                    {
                        itemNameText = "full";
                        ShowText(itemNameText);
                    }
                }
                else
                {
                    AudioSource.PlayClipAtPoint(pickUpSound, transform.position, 2f);
                    itemData.currentAmmo = 1;
                    itemNameText = itemData.ShortName;
                    ShowText(itemNameText);
                    EventBus.Instance.PickUpItem(itemData);
                    itemData.inInventory = true;
                    Destroy(gameObject);
                }
            }
            if(itemData.weaponType == WeaponType.Wearable)
            {
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position, 2f);
                Debug.Log("picked up " + itemData.ShortName);
                itemNameText = itemData.ShortName;
                itemData.inInventory = true;
                ShowText(itemNameText);
                EventBus.Instance.PickUpItem(itemData);
                Destroy(gameObject);
            }

        }
    }
    void ShowText(string itemNameText)
    {
        if(floatingTextBox)
        {
            GameObject prefab = Instantiate(floatingTextBox, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TMP_Text>().text = itemNameText;
            Destroy(prefab, nameLifeSpan);
        }
    }

    public ItemData GetItemData(){
        return itemData;
    }
}
