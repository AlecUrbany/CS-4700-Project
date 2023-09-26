using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addToInventory : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, playerMouth;
    public Quaternion defaultRotation;

    [SerializeField] private float pickUpRange;
    
    public bool equipped;

    private void Start()
    {

    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.Space))
            {
                PickUp();
            }
    }

    private void PickUp()
    {
        Debug.Log("Weapon Acquired");
        Weapon newItem = GetComponent<ItemObject>().item as Weapon;
        Inventory.AddItem(newItem);
    }
}
