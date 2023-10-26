using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelState
{
    public Vector3 playerPosition;
    public List<Vector3> enemyPositions;
    public List<string> droppedItems;
}
