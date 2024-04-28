using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameIntroUI : MonoBehaviour
{
    [SerializeField]
    private GameObject shhhhObj;
    [SerializeField]
    private GameObject crewmateObj;
    [SerializeField]
    private TextMeshProUGUI playerTypeTMP;

    [SerializeField]
    private Image gradation;

    [SerializeField]
    private IntroCharacter myCharacter;

    [SerializeField]
    private List<IntroCharacter> otherCharacters = new List<IntroCharacter>();

    [SerializeField]
    private Color crewColor;

    [SerializeField]
    private Color imposterColor;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public IEnumerator ShowIntroSequence()
    {
        shhhhObj.SetActive(true);
        yield return new WaitForSeconds(3f);
        shhhhObj.SetActive(false);

        ShowPlayerType();
        crewmateObj.SetActive(true);
    }

    public void ShowPlayerType()
    {
        var players = GameSystem.instance.GetPlayerList();

        InGameCharacterMover myPlayer = null;

        foreach (var player in players)
        {
            if (player.isOwned)
            {
                myPlayer = player;
            }
        }

        myCharacter.SetIntroCharacter(myPlayer.nickname, myPlayer.playerColor);
        myCharacter.gameObject.SetActive(true);

        if (myPlayer.playerType == EPlayerType.Imposter)
        {
            playerTypeTMP.text = "임포스터";
            playerTypeTMP.color = gradation.color = imposterColor;
            int i = 0;
            foreach(var player in players)
            {
                if(!player.isOwned && player.playerType == EPlayerType.Imposter)
                {
                    otherCharacters[i].SetIntroCharacter(player.nickname, player.playerColor);
                    otherCharacters[i].gameObject.SetActive(true);
                    ++i;
                }
            }
        }
        else if(myPlayer.playerType == EPlayerType.Crew)
        {
            playerTypeTMP.text = "크루원";
            playerTypeTMP.color = gradation.color = crewColor;
            int i = 0;
            foreach (var player in players)
            {
                if (!player.isOwned)
                {
                    otherCharacters[i].SetIntroCharacter(player.nickname, player.playerColor);
                    otherCharacters[i].gameObject.SetActive(true);
                    ++i;
                }
            }
        }
        //myCharacter.gameObject.SetActive(true);
    }

    public void Close()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer);
        }
        gameObject.SetActive(false);
    }
}
