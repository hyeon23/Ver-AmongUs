using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SotringSprite�� �����ϴ� Ŭ����(������Ʈ)
/// </summary>
public class SortingSprite : MonoBehaviour
{
    /// <summary>
    /// ���: Static
    /// ����(�̵�) ������Ʈ: Update
    /// </summary>
    public enum ESortingType
    {
        Static, Update, Count
    }

    [SerializeField]
    private ESortingType sortingType;

    private SpriteSorter sorter;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        sorter = FindObjectOfType<SpriteSorter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sorter.GetSortingOrder(gameObject);
    }

    private void Update()
    {
        //ESortingType.Update�� ��� ���������� sotingOrder ������Ʈ
        if(sortingType == ESortingType.Update)
        {
            spriteRenderer.sortingOrder = sorter.GetSortingOrder(gameObject);
        }
    }
}
