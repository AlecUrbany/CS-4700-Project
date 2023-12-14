using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventBus
{
    public bool canMove = true;
    public bool enemyCanMove = true;
    public bool hasMacguffin = false;
    public int numTimesAlertPhaseEntered = 0;
    public int numKilledEnemies = 0;
    public static EventBus Instance { get { if (theInstance == null) theInstance = new EventBus(); return theInstance; } }

    static EventBus theInstance;
    public event Action onOpenInventory;
    public event Action onCloseInventory;

    public event Action<ItemData> onPickUpItem;

    public event Action onPickUpMacguffin;

    public event Action onEnterAlertPhase;

    public event Action onEnemyKilled;

    public event Action onAnimationStart;

    public event Action onAnimationEnd;

    public event Action onLevelLoadStart;

    public event Action onLevelLoadEnd;
    
    public void OpenInventory()
    {
        onOpenInventory?.Invoke();
        canMove = false;
        enemyCanMove = false;
    }

    
    public void CloseInventory()
    {
        onCloseInventory?.Invoke();
        canMove = true;
        enemyCanMove = true;
    }

    public void PickUpItem(ItemData itemData)
    {
        //When function is invoked, it takes the item data.
        onPickUpItem?.Invoke(itemData);
    }

    public void PickUpMacguffin()
    {
        Debug.Log("Macguffin picked up");
        hasMacguffin = true;
    }

    public void EnterAlertPhase()
    {
        numTimesAlertPhaseEntered++;
    }

    public void EnemyKilled()
    {
        numKilledEnemies++;
    }

    public void AnimationStart()
    {
        onAnimationStart?.Invoke();
        canMove = false;
    }
    public void AnimationEnd()
    {
        onAnimationEnd?.Invoke();
        canMove = true;
    }
    public void LevelLoadStart()
    {
        onLevelLoadStart?.Invoke();
        canMove = false;
        enemyCanMove = false;
    }
    public void LevelLoadEnd()
    {
        onLevelLoadEnd?.Invoke();
        canMove = true;
        enemyCanMove = true;
    }


}
