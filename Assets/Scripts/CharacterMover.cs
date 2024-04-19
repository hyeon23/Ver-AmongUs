using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Org.BouncyCastle.Bcpg;

public class CharacterMover : NetworkBehaviour
{
    private Animator animator;

    public bool isMovable;

    [SyncVar]//네트워크 동기화
    public float speed = 2f;

    private SpriteRenderer spriteRenderer;

    [SyncVar(hook = nameof(SetPlayerColorHook))]
    //변수 동기화 + hook: SyncVar로 동기화된 변수가 Server에서 변경되면 hook으로 등록된 함수가 Client에서 호출
    public EPlayerColor playerColor;

    /// <summary>
    /// SyncVar [player Color(=색)]이 변경될 경우 클라이언트 측면에서 호출되는 함수
    /// 인자로 들어온 newColor의 색상으로 material의 Color를 변경해줌
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

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if(isOwned && isMovable)//권한 O & 이동 가능
        {
            bool isMove = false;

            if(PlayerSettings.controlType == EControlType.Mouse)
            {
                if (Input.GetMouseButton(0))
                {
                    //조작 방식에 따른 방향벡터
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;

                    //스프라이트 반전
                    if (dir.x < 0f) transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                    else if (dir.x > 0f) transform.localScale = new Vector3(0.5f, 0.5f, 1f);

                    //프레임 별 이동
                    transform.position += dir * speed * Time.deltaTime;

                    isMove = dir.magnitude != 0f;
                }
            }
            else if (PlayerSettings.controlType == EControlType.KeyboardMouse)
            {
                //조작 방식에 따른 방향벡터
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
                
                //스프라이트 반전
                if (dir.x < 0f) transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                
                //프레임 별 이동
                transform.position += dir * speed * Time.deltaTime;

                isMove = dir.magnitude != 0f;
            }

            animator.SetBool("isMove", isMove);
        }
    }
}
