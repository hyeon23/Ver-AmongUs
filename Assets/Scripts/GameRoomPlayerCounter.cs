using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class GameRoomPlayerCounter : NetworkBehaviour
{
    [SyncVar]
    private int minPlayer;
    [SyncVar]
    private int maxPlayer;
    [SerializeField]
    private TextMeshProUGUI playerCountTMP;

    public void UpdatePlayerCount()
    {
        var players = FindObjectsOfType<AmongUsRoomPlayer>();
        bool isStartable = players.Length >= minPlayer;
        playerCountTMP.color = isStartable ? Color.white : Color.red;
        playerCountTMP.text = $"{players.Length}/{maxPlayer}";
        LobbyUIManager.Instance.SetInteractableStartButton(isStartable);
    }

    private void Start()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as AmongUsRoomManager;
            minPlayer = manager.minPlayerCount;
            maxPlayer = manager.maxConnections;
        }
    }
}
