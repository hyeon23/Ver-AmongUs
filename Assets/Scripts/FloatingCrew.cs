using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FloatingCrew�� �����Ǵ� ������Ʈ
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
    /// floatingCrew�� �Ӽ� ����
    /// </summary>
    /// <param name="sprite">floatingCrew ��������Ʈ ����</param>
    /// <param name="playerColor">floatingCrew ���� ����</param>
    /// <param name="direction">�̵� ����</param>
    /// <param name="floatingSpeed">�̵� �ӵ�</param>
    /// <param name="rotateSpeed">ȸ�� �ӵ�</param>
    /// <param name="size">floatingCrew ũ��</param>
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
    /// �� �ӵ����� �̵� & ȸ��
    /// </summary>
    private void Update()
    {
        transform.position += direction * floatingSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, rotateSpeed));
    }
}
