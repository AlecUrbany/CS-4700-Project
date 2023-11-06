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
    private void OnTriggerStay(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (Input.GetButton("Interact"))
        {
            Debug.Log("picked up " + itemData.ShortName);
            itemData.currentAmmo = itemData.maxAmmo;
            itemNameText = itemData.ShortName;
            ShowText(itemNameText);
            EventBus.Instance.PickUpItem(itemData);
            Destroy(gameObject);
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
