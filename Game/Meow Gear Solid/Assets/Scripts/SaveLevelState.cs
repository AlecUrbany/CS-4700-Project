using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class LevelState
{
    public List<EnemyData> enemies = new List<EnemyData>();
    public List<ItemInformation> items = new List<ItemInformation>();
}

[System.Serializable]
public class EnemyData{
    public Vector3 position;
    public float health;
    public int aiState;
    public Vector3 startPosition;
    public float moveSpeed;
    public float patrolDistance;
    public float viewRadius;
}
[System.Serializable]
public class ItemInformation{
    public Vector3 itemPosition;
    public WeaponType type;
}
public class SaveLevelState : MonoBehaviour{
    public string levelToSave;
    List<Vector3> enemyPositon = new List<Vector3>();
    List<Vector3> enemyStartPosition = new List<Vector3>();
    List<float> enemyHealth = new List<float>();
    List<EnemyAIType> enemyAiType = new List<EnemyAIType>();
    private LevelState state;

    private void OnTriggerEnter(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }
        state = new LevelState();
        GetEnemyStates();
        GetDrops();
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
            visionCone cone = enemy.GetComponentInChildren<visionCone>();
            EnemyData enemyData = new EnemyData{
                position = enemy.transform.position,
                health = enemy.GetComponent<EnemyHealth>().currentHealth,
                startPosition = enemy.GetComponent<EnemyAI>().startPosition,
                aiState = (int)enemy.GetComponent<EnemyAI>().aiType,
                moveSpeed = enemy.GetComponent<EnemyAI>().moveSpeed,
                patrolDistance = enemy.GetComponent<EnemyAI>().patrolDistance,
                viewRadius = cone.viewRadius
            };
            state.enemies.Add(enemyData);
        }
    }

    public void GetDrops(){
        foreach(GameObject drops in GameObject.FindGameObjectsWithTag("Drops")){
            ItemPickUp itemPickUp = drops.GetComponent<ItemPickUp>();
                if(itemPickUp == null){
                    ConsumableItemPickup CitemPickUp = drops.GetComponent<ConsumableItemPickup>();
                    ItemInformation itemInformation = new ItemInformation{
                    itemPosition = drops.transform.position,
                    type = CitemPickUp.GetItemData().weaponType
                    };
                    state.items.Add(itemInformation);
                }
                else{
                    ItemInformation itemInformation = new ItemInformation{
                    itemPosition = drops.transform.position,
                    type = itemPickUp.GetItemData().weaponType
                    };
                    state.items.Add(itemInformation);
                }
        }
        
    }

}
