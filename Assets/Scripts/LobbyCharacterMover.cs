using Mirror;
using System.Collections.Generic;
using UnityEngine;

// LobbyCharacterMover 클래스 선언, CharacterMover 클래스를 상속받음
public class LobbyCharacterMover : CharacterMover
{
    // ownerNetId 변수 선언 및 SyncVar 속성 부여
    // ownerNetId가 변경될 때 호출되는 hook 함수 설정
    [SyncVar(hook = nameof(SetOwnerNetId_Hook))]
    public uint ownerNetId;

    // ownerNetId가 변경될 때 호출되는 hook 함수 정의
    public void SetOwnerNetId_Hook(uint _, uint newOwnerId)
    {
        // AmongUsRoomPlayer 클래스의 인스턴스들을 찾아서 배열로 저장
        var players = FindObjectsOfType<AmongUsRoomPlayer>();

        // 배열에 있는 모든 플레이어들에 대해 반복문 실행
        foreach (var player in players)
        {
            // ownerNetId가 특정 플레이어의 netId와 일치하는지 확인
            if (newOwnerId == player.netId)
            {
                // 일치하는 플레이어의 lobbyPlayerCharacter 변수에 현재 객체 설정
                player.lobbyPlayerCharacter = this;
                // 반복문 종료
                break;
            }
        }
    }

    // CompleteSpawn 함수 정의
    public void CompleteSpawn()
    {
        // 권한이 있는 경우에만 isMovable 변수를 true로 설정
        if (isOwned)
        {
            isMovable = true;
        }
    }
}