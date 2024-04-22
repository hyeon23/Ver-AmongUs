using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpawnPosition���� �����ϴ� Ŭ����
/// </summary>
public class SpawnPositions : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPositions;

    private int index;

    /// <summary>
    /// ���� index�� �ش��ϴ� SpawnPosition�� return���ش�.
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
