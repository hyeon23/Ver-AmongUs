using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Org.BouncyCastle.Bcpg;

public class CharacterMover : NetworkBehaviour
{
    private Animator animator;

    private bool isMovable;

    public bool IsMovable
    {
        get { return isMovable; }
        set
        {
            if (!value)
            {
                animator.SetBool("isMove", false);
            }
            isMovable = value;
        }
    }

    [SyncVar]//��Ʈ��ũ ����ȭ
    public float speed = 2f;

    private SpriteRenderer spriteRenderer;

    [SyncVar(hook = nameof(SetPlayerColorHook))]
    //���� ����ȭ + hook: SyncVar�� ����ȭ�� ������ Server���� ����Ǹ� hook���� ��ϵ� �Լ��� Client���� ȣ��
    public EPlayerColor playerColor;

    /// <summary>
    /// SyncVar [player Color(=��)]�� ����� ��� Ŭ���̾�Ʈ ���鿡�� ȣ��Ǵ� �Լ�
    /// ���ڷ� ���� newColor�� �������� material�� Color�� ��������
    /// </summary>
    public void SetPlayerColorHook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(newColor));
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        if (isOwned)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = 2.5f;
        }
    }

    /// <summary>
    /// �� �����Ӹ��� �̵�
    /// </summary>
    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// �� �����Ӹ��� ����Ǵ� �̵� �Լ�
    /// </summary>
    public void Move()
    {
        if(isOwned && IsMovable)//���� O & �̵� ����
        {
            bool isMove = false;

            if(PlayerSettings.controlType == EControlType.Mouse)
            {
                if (Input.GetMouseButton(0))
                {
                    //���� ��Ŀ� ���� ���⺤��
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;

                    //��������Ʈ ����
                    if (dir.x < 0f) transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                    else if (dir.x > 0f) transform.localScale = new Vector3(0.5f, 0.5f, 1f);

                    //������ �� �̵�
                    transform.position += dir * speed * Time.deltaTime;

                    isMove = dir.magnitude != 0f;
                }
            }
            else if (PlayerSettings.controlType == EControlType.KeyboardMouse)
            {
                //���� ��Ŀ� ���� ���⺤��
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
                
                //��������Ʈ ����
                if (dir.x < 0f) transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                
                //������ �� �̵�
                transform.position += dir * speed * Time.deltaTime;

                isMove = dir.magnitude != 0f;
            }

            animator.SetBool("isMove", isMove);
        }
    }
}
