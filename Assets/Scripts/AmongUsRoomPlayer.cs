using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/// <summary>
/// 대기실에 존재하는 플레이어에 관련된 `정보`를 담는 클래스
/// `움직임`을 다루는 클래스는 AmongUsPlayerMover
/// </summary>
public class AmongUsRoomPlayer : NetworkRoomPlayer
{
    private static AmongUsRoomPlayer myRoomPlayer;
    public static AmongUsRoomPlayer MyRoomPlayer
    {
        get
        {
            if (myRoomPlayer == null)
            {
                var players = FindObjectsOfType<AmongUsRoomPlayer>();
                foreach(var player in players)
                {
                    if (player.isOwned)
                    {
                        myRoomPlayer = player;
                    }
                }
            }
            return myRoomPlayer;
        }
    }

    //SyncVar: 네크워크 동기화 변수
    //hook: 해당 SyncVar가 변경되면, 자동으로 호출되는 함수
    [SyncVar(hook = nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        LobbyUIManager.Instance.CustomizeUI.UpdateColorButton();
    }

    //Lobby Player Character 캐릭터를 LobbyCharacterMover로 조작하기 위한 변수
    public CharacterMover lobbyPlayerCharacter;

    private void Start()
    {
        //NetworkRoomPlayer에 Start() 함수가 존재하기 때문에 이어 실행
        base.Start();

        if (isServer)
        {
            //RoomPlayer가 Server라면 -> LobbyPlayer 스폰
            SpawnLobbyPlayer();
        }
    }

    [Command]//Cmd 함수 작성 시, 이름 앞에 Cmd를 붙인다.
    public void CmdSetPlayerColor(EPlayerColor color)
    {
        playerColor = color;
        lobbyPlayerCharacter.playerColor = color;
    }

    /// <summary>
    /// LobbyPlayer를 스폰하는 함수
    /// </summary>
    private void SpawnLobbyPlayer()
    {
        var roomSlots = (NetworkManager.singleton as AmongUsRoomManager).roomSlots;

        EPlayerColor color = EPlayerColor.Red;

        for(int i = 0; i < (int)EPlayerColor.Count; ++i)
        {
            bool isFindSameColor = false;
            
            foreach(var roomPlayer in roomSlots)
            {
                var amongUsRoomPlayer = roomPlayer as AmongUsRoomPlayer;
                //동일한 플레이어가 아닌데, 플레이어 칼라가 같은 경우 -> 같은 색 처리(중복 제거)
                if(amongUsRoomPlayer.playerColor == (EPlayerColor)i && roomPlayer.netId != netId)
                {
                    isFindSameColor = true;
                    break;
                }
            }

            //중복된 색이 아닐 경우
            if (!isFindSameColor)
            {
                color = (EPlayerColor)i;
                break;
            }
        }

        //-> 플레이어 색으로 설정
        playerColor = color;

        //설정된 정보를 기반으로 LobbyPlayer 스폰
        Vector3 spawnPos = FindObjectOfType<SpawnPositions>().GetSpawnPosition();

        var player = Instantiate(AmongUsRoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMover>();

        NetworkServer.Spawn(player.gameObject, connectionToClient);

        //생성된 LobbyPlayer의 정보 설정
        player.ownerNetId = netId;

        player.playerColor = color;
    }
}
