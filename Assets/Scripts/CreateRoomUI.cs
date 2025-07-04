using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> crewImgs;
    [SerializeField]
    private List<Button> imposterCountButtons;
    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    private void Start()
    {
        for(int i = 0; i < crewImgs.Count; ++i)
        {
            Material matInst = Instantiate(crewImgs[i].material);
            crewImgs[i].material = matInst;
        }

        roomData = new CreateGameRoomData(1, 10);
        UpdateCrewImages();
    }


    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        for (int i = 0; i < imposterCountButtons.Count; ++i)
        {
            if (i == count - 1)
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        int limitMaxPlayer = (count == 1) ? 4 : ((count == 2) ? 7 : 9);

        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }
        for(int i = 0; i < maxPlayerCountButtons.Count; ++i)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            
            if(i < limitMaxPlayer - 4)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }
    }

    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        for(int i = 0; i < maxPlayerCountButtons.Count; ++i)
        {
            if(i == count - 4)
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        UpdateCrewImages();
    }

    private void UpdateCrewImages()
    {
        for(int i = 0; i < crewImgs.Count; ++i)
        {
            crewImgs[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterCount = roomData.imposterCount;
        int idx = 0;
        while(imposterCount != 0)
        {
            if(idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }

            if (crewImgs[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
            {
                crewImgs[idx].material.SetColor("_PlayerColor", Color.red);
                --imposterCount;
            }
            ++idx;
        }

        for(int i = 0; i < crewImgs.Count; ++i)
        {
            if(i < roomData.maxPlayerCount)
            {
                crewImgs[i].gameObject.SetActive(true);
            }
            else
            {
                crewImgs[i].gameObject.SetActive(false);
            }
        }
    }

    public void CreateRoom()
    {
        var manager = NetworkRoomManager.singleton as AmongUsRoomManager;

        //방 설정  작업 처리
        manager.minPlayerCount = roomData.imposterCount == 1 ? 4 : roomData.imposterCount == 2 ? 7 : 9;
        manager.imposterCount = roomData.imposterCount;
        manager.maxConnections = roomData.maxPlayerCount;//자체적으로 존재하는 최대 연결 수 변수
        
        manager.StartHost();
    }
}

public class CreateGameRoomData
{
    public int imposterCount;
    public int maxPlayerCount;

    public CreateGameRoomData(int cic, int cmpc)
    {
        this.imposterCount = cic;
        this.maxPlayerCount = cmpc;
    }
}
