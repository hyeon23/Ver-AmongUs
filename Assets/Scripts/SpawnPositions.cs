using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpawnPosition들을 관리하는 클래스
/// </summary>
public class SpawnPositions : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPositions;

    private int index;

    /// <summary>
    /// 현재 index에 해당하는 SpawnPosition을 return해준다.
    /// </summary>
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
