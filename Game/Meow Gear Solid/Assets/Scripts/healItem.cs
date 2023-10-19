using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healItem : MonoBehaviour
{
    public float pickUpRange;
    public Transform player;
    public float healAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E)) Heal();
    }
    private void Heal()
    {
        PlayerHealth playerScript = player.GetComponent<PlayerHealth>();
        playerScript.HealHealth(healAmount);
        gameObject.SetActive(false);
    }
}
