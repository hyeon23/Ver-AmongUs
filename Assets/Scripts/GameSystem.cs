using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//GameSystem Start 시점에서 모든 플레이어가 생성되었음이 보장되지 않음
//InGameCharacterMover들이 GameSystem에 접근해, 플레이어임을 등록하도록 함
public class GameSystem : NetworkBehaviour
{
    public static GameSystem instance;

    private List<InGameCharacterMover> players = new List<InGameCharacterMover>();

    [SerializeField]
    private Transform spawnTransform;

    [SerializeField]
    private float spawnDistance;

    public void AddPlayer(InGameCharacterMover player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }

    private IEnumerator GameReady()
    {
        var manager = NetworkManager.singleton as AmongUsRoomManager;
        while (manager.roomSlots.Count != players.Count)
        {
            yield return null;
        }

        //등록 완료 = Room Manager에 등록한 임포스터 수만큼 임포스터 선출
        for(int i = 0; i < manager.imposterCount; i++)
        {
            var player = players[Random.Range(0, players.Count)];
            if(player.playerType != EPlayerType.Imposter)
            {
                player.playerType = EPlayerType.Imposter;
            }
            else
            {
                i--;//다시 뽑기
            }
        }

        for(int i = 0; i < players.Count; ++i)
        {
            float radian = (2f * Mathf.PI) / players.Count;//각도
            radian *= i;
            players[i].RpcTeleport(spawnTransform.position + 
                new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f) * spawnDistance);
        }

        yield return new WaitForSeconds(2f);

        RpcStartGame();
    }

    [ClientRpc]
    private void RpcStartGame() 
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {


        yield return StartCoroutine(IngameUIManager.instance.IngameIntroUI.ShowIntroSequence());

        InGameCharacterMover myCharacter = null;

        foreach(var player in players)
        {
            if (player.isOwned)
            {
                myCharacter = player;
                break;
            }
        }

        //나 포함 임포스터면 빨간색 표시
        foreach(var player in players)
        {
            player.SetNicknameColor(myCharacter.playerType);
        }

        yield return new WaitForSeconds(3f);

        IngameUIManager.instance.IngameIntroUI.Close();
    }

    public List<InGameCharacterMover> GetPlayerList()
    {
        return players;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (isServer)
        {
            StartCoroutine(GameReady());
        }
    }
}
