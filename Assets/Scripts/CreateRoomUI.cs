using System.Collections;
using System.Collections.Generic;
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

    private void UpdateCrewImages()
    {
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
