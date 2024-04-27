using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroCharacter : MonoBehaviour
{
    [SerializeField]
    private Image character;

    [SerializeField]
    private TextMeshProUGUI nicknameTMP;

    public void SetIntroCharacter(string name, EPlayerColor color)
    {
        var mat = Instantiate(character.material);
        character.material = mat;

        nicknameTMP.text = name;
        character.material.SetColor("_PlayerColor", PlayerColor.GetColor(color));
    }

}
