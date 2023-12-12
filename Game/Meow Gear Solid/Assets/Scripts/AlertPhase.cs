using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class AlertPhase : MonoBehaviour
{
    public GameObject miniMap;
    public GameObject AlertInfo;
    public TextMeshProUGUI TimerText;
    private bool inAlertPhase;
    private double timeRemaining = 0;
    private double alertDuration = 5;
    private Transform player;
    private Vector3 lastKnownPosition; 

    public bool getInAlertPhase() {
        return inAlertPhase;
    }

    public double getTimeRemaining() {
        return timeRemaining;
    }

    public Vector3 getLastKnownPosition() {
        return lastKnownPosition;
    }

    public void updateCanSeePlayer(bool canSeePlayer) 
    {
        if (canSeePlayer) 
        {
            lastKnownPosition = player.position;

            if (!inAlertPhase) {
                // We are entering alert phase
                EnterAlertPhase();
                
            }

            if (!EventBus.Instance.hasMacguffin) 
            {
                timeRemaining = alertDuration;
                TimerText.text = string.Format("{0:00}", timeRemaining);
            }
        }
    }

    public void MacguffinAquired()
    {
        if (!inAlertPhase) {
            EnterAlertPhase();
        }

        lastKnownPosition = player.position;
        timeRemaining = 999;
        TimerText.text = "∞";
    }

    private void EnterAlertPhase() 
    {
        EventBus.Instance.EnterAlertPhase();
        inAlertPhase = true;
        miniMap.SetActive(false);
        AlertInfo.SetActive(true);
        // TODO: Add Enemy spawn and other stuff that happens when first entering alert phase
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (inAlertPhase) 
        {
            if (!EventBus.Instance.hasMacguffin) 
            {
                timeRemaining -= Time.deltaTime;
                TimerText.text = string.Format("{0:00}", timeRemaining);
            }

            if (timeRemaining <= 0)
            {
                // Exit AlertPhase
                timeRemaining = 0;
                inAlertPhase = false;
                miniMap.SetActive(true);
                AlertInfo.SetActive(false);
                // TODO: Implement destroy AlertPhase enemies
                return;
            }

            // TODO: Implement update of AlertPhase enemies to ensure there are always 4 of them
        }
    }
}
