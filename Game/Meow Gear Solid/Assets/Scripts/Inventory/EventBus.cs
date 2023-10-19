using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventBus
{
    public static EventBus Instance { get { if (theInstance == null) theInstance = new EventBus(); return theInstance; } }

    static EventBus theInstance;
    public event Action onOpenInventory;
    public event Action onCloseInventory;

    public event Action<ItemData> onPickUpItem;
    
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
