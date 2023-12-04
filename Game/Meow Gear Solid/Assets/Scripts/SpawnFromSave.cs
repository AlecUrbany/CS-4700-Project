using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class GameData{
    public EnemyDataLoad[] enemies;
    public DropData[] drops;
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
    public Vector3 position;
    public string itemName;
}

public class SpawnFromSave : MonoBehaviour{
    public string defaultSpawnPointName = "DefaultSpawn"; //Default location
    public Transform player;
    public GameObject enemyPrefab;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(GameObject.FindGameObjectsWithTag("Player").Length > 1){
            
        }
        string sceneName = SceneManager.GetActiveScene().name;
        string folderPath = Path.Combine(Application.dataPath, "../LevelStates");
        string fileName = sceneName + ".json";
        string filePath = Path.Combine(folderPath, fileName);
        if(File.Exists(filePath)){
            Debug.Log("File exists");
            DestroyGameObjectsWithTag("Enemy");
            //DestroyGameObjectsWithTag("Drop");
            string jsonText = System.IO.File.ReadAllText(filePath);
            GameData gameData = JsonUtility.FromJson<GameData>(jsonText);
            foreach (EnemyDataLoad enemyData in gameData.enemies){
                GameObject enemy = Instantiate(enemyPrefab, enemyData.position, Quaternion.identity);
                EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
                enemyAI.player = player;
                enemyAI.chaseStartPosition = enemyData.startPosition;
                enemyAI.aiType = (EnemyAIType)enemyData.aiState;
                enemyAI.patrolDistance = enemyData.patrolDistance;
                enemyAI.moveSpeed = enemyData.moveSpeed;
                visionCone cone = enemy.GetComponentInChildren<visionCone>();
                cone.viewRadius = enemyData.viewRadius;
                EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                health.currentHealth = enemyData.health;
            }

        }
        string targetSpawnPointName = PlayerPrefs.GetString("TargetSpawnPoint", defaultSpawnPointName);
        Transform targetSpawnPoint = GameObject.Find(targetSpawnPointName).transform;
        player.position = targetSpawnPoint.position - new Vector3(0, 0, 3);
    }

    void DestroyGameObjectsWithTag(string tag){
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject gameObject in gameObjects){
            Destroy(gameObject);
        }
    }
}
