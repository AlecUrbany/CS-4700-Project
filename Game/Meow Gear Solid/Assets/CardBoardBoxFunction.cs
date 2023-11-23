using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardBoxFunction : MonoBehaviour
{
    public bool isMoving;
    public bool alertPhase;
    void Update()
    {
        alertPhase = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<AlertPhase>().inAlertPhase;
        if(Input.anyKey || alertPhase == true)
        {
            transform.gameObject.layer = 9; //player layer
            isMoving = true;
        }
        else
        {
            transform.gameObject.layer = 8; //obstacle layer
            isMoving = false;
        }
    }

}
