using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleItem : MonoBehaviour
{
    [SerializeField]
    private GameObject inactiveObject;
    // Start is called before the first frame update
    void Start()
    {
        if (AmongUsRoomPlayer.MyRoomPlayer != null && !AmongUsRoomPlayer.MyRoomPlayer.isServer)
        {
            inactiveObject.SetActive(false);
        }
    }
}
