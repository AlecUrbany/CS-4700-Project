using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public string defaultSpawnPointName = "DefaultSpawn"; //Default location
    public Transform player;

    private void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length > 1){
            Destroy(gameObject); //Check if the player from the last scene is in this one and destories
        }
        else{
            string targetSpawnPointName = PlayerPrefs.GetString("TargetSpawnPoint", defaultSpawnPointName);
            Transform targetSpawnPoint = GameObject.Find(targetSpawnPointName).transform;
            player.position = targetSpawnPoint.position - new Vector3(3, 0.75f, 0);
        }
    }
}
