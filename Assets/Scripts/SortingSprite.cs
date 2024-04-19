using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SotringSprite에 부착하는 클래스(컴포넌트)
/// </summary>
public class SortingSprite : MonoBehaviour
{
    /// <summary>
    /// 배경: Static
    /// 동적(이동) 오브젝트: Update
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
        //ESortingType.Update의 경우 지속적으로 sotingOrder 업데이트
        if(sortingType == ESortingType.Update)
        {
            spriteRenderer.sortingOrder = sorter.GetSortingOrder(gameObject);
        }
    }
}
