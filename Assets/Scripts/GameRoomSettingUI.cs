using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomSettingUI : SettingUI
{
    public void Open()
    {
        AmongUsRoomPlayer.MyRoomPlayer.lobbyPlayerCharacter.isMovable = false;
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        base.Close();
        AmongUsRoomPlayer.MyRoomPlayer.lobbyPlayerCharacter.isMovable = true;
    }

    public void ExitGameRoom()
    {
        //Host Migration
        //-ȣ��Ʈ�� ������ ���, Ŭ���̾�Ʈ�� Host�� ������ ���ӹ޴� ��� ���� �ʿ�
        var manager = AmongUsRoomManager.singleton;
        if(manager.mode == Mirror.NetworkManagerMode.Host)
        {
            manager.StopHost();
        }
        else if(manager.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
    }

    public void ReturnGameRoom()
    {

    }
}
