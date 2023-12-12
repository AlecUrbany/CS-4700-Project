using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class AlertPhase : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject miniMap;
    public GameObject AlertInfo;
    public TextMeshProUGUI TimerText;
    public Vector3 enemySpawnPosition;
    private List<GameObject> alertEnemies;
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
        alertEnemies = new List<GameObject>();
        alertEnemies.Add(createEnemy());
        alertEnemies.Add(createEnemy());
        alertEnemies.Add(createEnemy());
        alertEnemies.Add(createEnemy());
    }

    private GameObject createEnemy() {
        return Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
    }

    private void updateEnemies() {
        alertEnemies.RemoveAll(enemyPrefab => enemyPrefab == null);
        while (alertEnemies.Count < 4) {
            alertEnemies.Add(createEnemy());
        }
    }

    private void destroyEnemies() {
        foreach (GameObject enemy in alertEnemies) {
            Destroy(enemy);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // TODO: Determine better way to set enemySpawnPosition (will likely be differnt for each level)
        enemySpawnPosition = new Vector3(0, 5, 0);
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
                destroyEnemies();
                return;
            }

            // TODO: Implement update of AlertPhase enemies to ensure there are always 4 of them
            updateEnemies();
        }
    }
}
