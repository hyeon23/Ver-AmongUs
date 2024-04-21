using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeLabtop : MonoBehaviour
{
    [SerializeField]
    private Sprite useButtonSprite;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var inst = Instantiate(spriteRenderer.material);
        spriteRenderer.material = inst;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.isOwned)
        {
            spriteRenderer.material.SetFloat("_Highlighted", 1f);
            LobbyUIManager.Instance.SetUseButton(useButtonSprite, OnClickUseButton);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.isOwned)
        {
            spriteRenderer.material.SetFloat("_Highlighted", 0f);
            LobbyUIManager.Instance.UnSetUseButton();
        }
    }

    public void OnClickUseButton()
    {
        LobbyUIManager.Instance.CustomizeUI.Open();
    }
}
