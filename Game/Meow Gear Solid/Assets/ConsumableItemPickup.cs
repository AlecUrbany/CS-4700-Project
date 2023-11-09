using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ConsumableItemPickup : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextBox;

    private string itemNameText;

    public float nameLifeSpan = .5f;
    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemData gunAmmo;
    private void OnTriggerStay(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (Input.GetButton("Interact"))
        {
            if(gunAmmo.currentAmmo < gunAmmo.maxAmmo)
            {
                Debug.Log("picked up " + itemData.ShortName);
                if(itemData.currentAmmo + gunAmmo.currentAmmo <= gunAmmo.maxAmmo)
                {
                    gunAmmo.currentAmmo = itemData.currentAmmo + gunAmmo.currentAmmo;
                }
                else
                {
                    gunAmmo.currentAmmo = gunAmmo.maxAmmo;
                }
                
                itemNameText = itemData.ShortName;
                ShowText(itemNameText);
                Destroy(gameObject);
            }
            else
            {
                itemNameText = "FULL";
                ShowText(itemNameText);
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
}
