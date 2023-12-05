using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardBoxFunction : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform box;
    public Transform playerHead;
    public Transform playerStatic;
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
            box.transform.parent = playerHead;
            box.transform.position = playerStatic.position;
        }

        else
        {
            if(alertPhase == true)
            {
                transform.gameObject.layer = 9; //player layer 
                isMoving = false;
                box.transform.parent = playerStatic;
                box.transform.position = playerStatic.position;
                box.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            else
            {
                transform.gameObject.layer = 8; //obstacle layer
                isMoving = false;
                playerAnimator.SetBool("IsMoving", isMoving);
                box.transform.parent = playerStatic;
                box.transform.position = playerStatic.position;
                box.transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
        }
    }
    void Start()
    {
       playerHead = GameObject.FindGameObjectWithTag("Head").GetComponent<Transform>();
       playerStatic = GameObject.FindGameObjectWithTag("PlayerStatic").GetComponent<Transform>();
    }

}
