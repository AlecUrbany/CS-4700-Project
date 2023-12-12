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
    public AudioClip pickUpSound;
    public bool textShown;
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
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position, 2f);
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
                if(textShown == false)
                {
                    itemNameText = "FULL";
                    ShowText(itemNameText);
                    textShown = true;
                    StartCoroutine("Timeout");
                }
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
    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(1.5f);
        textShown = false;

    }
}
