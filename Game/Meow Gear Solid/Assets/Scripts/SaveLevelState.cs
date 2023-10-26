using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class SaveLevelState : MonoBehaviour{
    public string levelToSave;
    public void Start(){
    LevelState state = new LevelState();
    state.enemyPositions = GetEnemyPositions();
    state.collectedItems = GetDrops();

    string jsonState = JsonUtility.ToJson(state);
    PlayerPrefs.SetString(levelToSave, jsonState);
    PlayerPrefs.Save();
    }

    public void GetEnemyPositions(){
        List<Enemy> enemyPositions = new List<Vector3>();
        foreach (Enemy enemy in enemiesInLevel)
        {
            enemyPositions.Add(enemy.transform.position);
        }
        return enemyPositions;
    }

}
*/