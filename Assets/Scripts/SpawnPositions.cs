using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPositions;

    private int index;

    public Vector3 GetSpawnPosition()
    {
        Vector3 pos = spawnPositions[index++].position;
        if(index >= spawnPositions.Length)
        {
            index = 0;
        }
        return pos;
    }
}
