using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FloatingCrew에 부착되는 컴포넌트
/// </summary>
public class FloatingCrew : MonoBehaviour
{
    public EPlayerColor playerColor;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private float floatingSpeed;
    private float rotateSpeed;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// floatingCrew의 속성 설정
    /// </summary>
    /// <param name="sprite">floatingCrew 스프라이트 설정</param>
    /// <param name="playerColor">floatingCrew 색상 설정</param>
    /// <param name="direction">이동 방향</param>
    /// <param name="floatingSpeed">이동 속도</param>
    /// <param name="rotateSpeed">회전 속도</param>
    /// <param name="size">floatingCrew 크기</param>
    public void SetFloatingCrew(Sprite sprite, EPlayerColor playerColor, Vector3 direction, float floatingSpeed, float rotateSpeed, float size)
    {
        this.playerColor = playerColor;
        this.direction = direction;
        this.floatingSpeed = floatingSpeed;
        this.rotateSpeed = rotateSpeed;

        this.spriteRenderer.sprite = sprite;
        this.spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        transform.localScale = Vector3.one * size;
        spriteRenderer.sortingOrder = (int)Mathf.Lerp(1, 32767, size);
    }

    /// <summary>
    /// 매 속도마다 이동 & 회전
    /// </summary>
    private void Update()
    {
        transform.position += direction * floatingSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, rotateSpeed));
    }
}
