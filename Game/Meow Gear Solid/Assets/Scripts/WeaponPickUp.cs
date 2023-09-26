using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, playerMouth;
    public Quaternion defaultRotation;

    public float pickUpRange;
    
    public bool equipped;
    public static bool slotFull;
    public GameObject bullet; 
    public int test;


    private void Start()
    {
        rb.isKinematic = false;
        coll.isTrigger = false;
        slotFull = false;
        defaultRotation = transform.localRotation;
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

        if(equipped) transform.position = playerMouth.position;
    }

    private void PickUp()
    {
        equipped = true;
        transform.position = playerMouth.position;
        rb.isKinematic = true;
        transform.localRotation = defaultRotation;
        GunFunctions gunScript = bullet.GetComponent<GunFunctions>();
        if (gunScript != null){
                gunScript.enabled = true;
        }
    }
    private void Drop()
    {
        equipped = false;
        rb.isKinematic = false;
        GunFunctions gunScript = bullet.GetComponent<GunFunctions>();
        if (gunScript != null){
                gunScript.enabled = false;
        }
    }
}
