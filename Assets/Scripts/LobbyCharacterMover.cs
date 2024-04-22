using Mirror;
using System.Collections.Generic;
using UnityEngine;

// LobbyCharacterMover Ŭ���� ����, CharacterMover Ŭ������ ��ӹ���
public class LobbyCharacterMover : CharacterMover
{
    // ownerNetId ���� ���� �� SyncVar �Ӽ� �ο�
    // ownerNetId�� ����� �� ȣ��Ǵ� hook �Լ� ����
    [SyncVar(hook = nameof(SetOwnerNetId_Hook))]
    public uint ownerNetId;

    // ownerNetId�� ����� �� ȣ��Ǵ� hook �Լ� ����
    public void SetOwnerNetId_Hook(uint _, uint newOwnerId)
    {
        // AmongUsRoomPlayer Ŭ������ �ν��Ͻ����� ã�Ƽ� �迭�� ����
        var players = FindObjectsOfType<AmongUsRoomPlayer>();

        // �迭�� �ִ� ��� �÷��̾�鿡 ���� �ݺ��� ����
        foreach (var player in players)
        {
            // ownerNetId�� Ư�� �÷��̾��� netId�� ��ġ�ϴ��� Ȯ��
            if (newOwnerId == player.netId)
            {
                // ��ġ�ϴ� �÷��̾��� lobbyPlayerCharacter ������ ���� ��ü ����
                player.lobbyPlayerCharacter = this;
                // �ݺ��� ����
                break;
            }
        }
    }

    // CompleteSpawn �Լ� ����
    public void CompleteSpawn()
    {
        // ������ �ִ� ��쿡�� isMovable ������ true�� ����
        if (isOwned)
        {
            IsMovable = true;
        }
    }
}