using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    public Weapon testWeapon;
    void Start()
    {
            InitVariables();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            AddItem(testWeapon);

        }
    }
    public void AddItem(Weapon newItem)
    {
        int newItemIndex = (int)newItem.weaponIndex;

        if(weapons[newItemIndex] != null)
        {
            Debug.Log("you already have an item equipped to slot " + newItemIndex);
        }
        else
        {
            weapons[newItemIndex] = newItem;
        }
    }

    public void RemoveItem(int index)
    {
        weapons[index] = null;
    }

    public Weapon GetItem(int index)
    {
        return weapons[index];
    }

    private void InitVariables()
    {
        weapons = new Weapon[3];
    }
}
