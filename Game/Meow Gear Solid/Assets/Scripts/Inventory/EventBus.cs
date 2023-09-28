using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance {get; private set; }

    public event Action onOpenInventory;
    public event Action onCloseInventory;

    public event Action<ItemData> onPickUpItem;
    
    private void Awake()
    {
        Instance = this;
    }
    public void OpenInventory()
    {
        onOpenInventory?.Invoke();
    }

    
    public void CloseInventory()
    {
        onCloseInventory?.Invoke();
    }

    public void PickUpItem(ItemData itemData)
    {
        //When function is invoked, it takes the item data.
        onPickUpItem?.Invoke(itemData);
    }
}
