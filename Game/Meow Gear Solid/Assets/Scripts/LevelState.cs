using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelState
{
    public List<Vector3> position;
    public List<float> health;
    public List<EnemyAIType> aiState;
    public List<Vector3> startPosition;
    public List<string> droppedItems;
}
