using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class GameData{
    public EnemyDataLoad[] enemies;
    public DropData[] items;
}

[System.Serializable]
public class EnemyDataLoad{
    public Vector3 position;
    public float health;
    public int aiState;
    public Vector3 startPosition;
    public float moveSpeed;
    public float patrolDistance;
    public float viewRadius;
}

[System.Serializable]
public class DropData{
    public Vector3 itemPosition;
    public WeaponType type;
}

public class SpawnFromSave : MonoBehaviour{
    public string defaultSpawnPointName = "DefaultSpawn"; //Default location
    public Transform player;
    public GameObject enemyPrefab;
    public GameObject pistolPrefab;
    public GameObject healthPrefab;
    public GameObject ammoPrefab;
    public GameObject tranqPrefab;
    public GameObject wearPrefab;
    private SpawnSide spawnSide;
    private float spawnOffset;
    private float hortizontalOffset;


    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        string sceneName = SceneManager.GetActiveScene().name;
        string folderPath = Path.Combine(Application.dataPath, "../LevelStates");
        string fileName = sceneName + ".json";
        string filePath = Path.Combine(folderPath, fileName);
        if(File.Exists(filePath)){
            DestroyGameObjectsWithTag("Enemy");
            DestroyGameObjectsWithTag("Drops");
            //DestroyGameObjectsWithTag("Drop");
            string jsonText = System.IO.File.ReadAllText(filePath);
            GameData gameData = JsonUtility.FromJson<GameData>(jsonText);
            Debug.Log(gameData.items);
            foreach (EnemyDataLoad enemyData in gameData.enemies){
                GameObject enemy = Instantiate(enemyPrefab, enemyData.position, Quaternion.identity);
                EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
                enemyAI.player = player;
                enemyAI.startPosition = enemyData.startPosition;
                enemyAI.aiType = (EnemyAIType)enemyData.aiState;
                enemyAI.patrolDistance = enemyData.patrolDistance;
                enemyAI.moveSpeed = enemyData.moveSpeed;
                visionCone cone = enemy.GetComponentInChildren<visionCone>();
                cone.viewRadius = enemyData.viewRadius;
                EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                health.currentHealth = enemyData.health;
            }
            foreach(DropData dropData in gameData.items){
                switch(dropData.type){
                    case WeaponType.Pistol:
                        GameObject PistolDrop = Instantiate(pistolPrefab, dropData.itemPosition, Quaternion.identity);
                        break;
                    case WeaponType.Tranquilizer:
                        GameObject TranqDrop = Instantiate(tranqPrefab, dropData.itemPosition, Quaternion.identity);
                        break;
                    case WeaponType.Healing:
                        GameObject HealthDrop = Instantiate(healthPrefab, dropData.itemPosition, Quaternion.identity);
                        break;
                    case WeaponType.Wearable:
                        GameObject WearDrop = Instantiate(wearPrefab, dropData.itemPosition, Quaternion.identity);
                        break;
                    case WeaponType.Consumable:
                        GameObject AmmoDrop = Instantiate(ammoPrefab, dropData.itemPosition, Quaternion.identity);
                        break;
                    }
            }

        }
        string targetSpawnPointName = PlayerPrefs.GetString("TargetSpawnPoint", defaultSpawnPointName);
        Transform targetSpawnPoint = GameObject.Find(targetSpawnPointName).transform;
        LoadLevel loadLevelScript = GameObject.Find(targetSpawnPointName).GetComponent<LoadLevel>();
        spawnSide = loadLevelScript.thisPortalsSpawnSide;
        spawnOffset = loadLevelScript.thisPortalsSpawnOffset;
        hortizontalOffset = loadLevelScript.thisPortalsHortizontalOffset;
        switch (spawnSide){
            case SpawnSide.Left:
                player.position = targetSpawnPoint.position - new Vector3(spawnOffset, hortizontalOffset, 0);
                break;
            case SpawnSide.Right:
                player.position = targetSpawnPoint.position - new Vector3(-spawnOffset, hortizontalOffset, 0);
                break;
            case SpawnSide.Behind:
                player.position = targetSpawnPoint.position - new Vector3(0, hortizontalOffset, -spawnOffset);
                break;
            case SpawnSide.Front:
                player.position = targetSpawnPoint.position - new Vector3(0, hortizontalOffset, spawnOffset);
                break;
        }
    }

    void DestroyGameObjectsWithTag(string tag){
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject gameObject in gameObjects){
            Destroy(gameObject);
        }
    }
}
