using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveLevelState : MonoBehaviour{
    public string levelToSave;
    List<Vector3> enemyPositon = new List<Vector3>();
    List<Vector3> enemyStartPosition = new List<Vector3>();
    List<float> enemyHealth = new List<float>();
    List<EnemyAIType> enemyAiType = new List<EnemyAIType>();
    private void OnTriggerEnter(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }
        LevelState state = new LevelState();
        GetEnemyStates();
        GetDrops();
        state.position = enemyPositon;
        state.health = enemyHealth;
        state.aiState = enemyAiType;
        state.startPosition = enemyStartPosition;
        string jsonState = JsonUtility.ToJson(state);
        string parentDirectory = Directory.GetParent(Application.dataPath).FullName;
        string folderPath = Path.Combine(parentDirectory, "LevelStates");
        Directory.CreateDirectory(folderPath);
        string fileName = levelToSave + ".json";
        string filePath = Path.Combine(folderPath, fileName);
        Debug.Log(filePath);

        File.WriteAllText(filePath, jsonState); // Save the JSON data to a file

    }

    private void GetEnemyStates(){
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            enemyPositon.Add(enemy.transform.position);
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            enemyHealth.Add(health.currentHealth);
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            enemyStartPosition.Add(enemyAI.chaseStartPosition);
            enemyAiType.Add(enemyAI.aiType);
        }
    }

    public void GetDrops(){
        
    }

}
