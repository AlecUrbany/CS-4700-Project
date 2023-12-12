using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MacguffinPickup : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextBox;
    private string itemNameText;

    public float nameLifeSpan = 1.5f;
    [SerializeField] private ItemData itemData;
    public bool alertPhase;
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
                Debug.Log("picked up " + itemData.ShortName);
                itemNameText = itemData.ShortName;
                itemData.inInventory = true;
                ShowText(itemNameText);
                EventBus.Instance.PickUpItem(itemData);
                EventBus.Instance.PickUpMacguffin();
                Destroy(gameObject);
                GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<AlertPhase>().MacguffinAquired();
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
