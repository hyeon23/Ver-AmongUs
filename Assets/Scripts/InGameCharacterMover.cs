using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCharacterMover : CharacterMover
{
    public override void Start()
    {
        base.Start();

        if (isOwned)
        {
            IsMovable = true;

            var myRoomPlayer = AmongUsRoomPlayer.MyRoomPlayer;

            CmdSetPlayerCharacter(myRoomPlayer.nickname, myRoomPlayer.playerColor);
        }
    }

    [Command]
    private void CmdSetPlayerCharacter(string nickname, EPlayerColor playerColor)
    {
        this.nickname = nickname;
        this.playerColor = playerColor;
    }
}
