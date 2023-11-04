using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
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
            EventBus.Instance.PickUpItem(itemData);
            Destroy(gameObject);
        }
    }
}
