using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomSettingUI : SettingUI
{
    public void ExitGameRoom()
    {
        //Host Migration
        //-호스트가 종료할 경우, 클라이언트가 Host의 권한을 위임받는 기능 개발 필요
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
