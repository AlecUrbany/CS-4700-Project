using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject inventoryViewObject;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Toggle Inventory"))
        {
            if(inventoryViewObject.activeSelf == true)
            {
                inventoryViewObject.SetActive(false);
            }
            
            else
            {
                inventoryViewObject.SetActive(true);    
            }
        }
                
    }

    public void OnSelected(ItemSlot selectedSlot)
    {
        if (selectedSlot.itemData == null)
        {
            itemNameText.ClearMesh();
            itemDescText.ClearMesh();
            return;
        }
        itemNameText.SetText(selectedSlot.itemData.Name);
        itemDescText.SetText(selectedSlot.itemData.Description[0]);
    }
}
