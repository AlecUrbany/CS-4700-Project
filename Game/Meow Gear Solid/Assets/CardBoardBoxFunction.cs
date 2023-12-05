using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardBoxFunction : MonoBehaviour
{
    public Animator playerAnimator;
    public bool wearingBox;
    public bool isMoving;
    public bool isVisible;
    public bool alertPhase;
    
    void Update()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        alertPhase = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<AlertPhase>().inAlertPhase;


        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            transform.gameObject.layer = 9; //player layer
            isMoving = true;
            playerAnimator.SetBool("IsMoving", isMoving);
        }

        else
        {
            if(alertPhase == true)
            {
                transform.gameObject.layer = 9; //player layer 
                isMoving = false;
            }

            else
            {
                transform.gameObject.layer = 8; //obstacle layer
                isMoving = false;
                playerAnimator.SetBool("IsMoving", isMoving);
            }
        }
    }
    void Start()
    {
        wearingBox = true;
    }

}
