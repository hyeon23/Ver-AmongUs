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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

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
                    if (dir.x < 0f) transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
                    else if (dir.x > 0f) transform.localScale = new Vector3(0.75f, 0.75f, 1f);

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
                if (dir.x < 0f) transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
                else if (dir.x > 0f) transform.localScale = new Vector3(0.75f, 0.75f, 1f);
                
                //프레임 별 이동
                transform.position += dir * speed * Time.deltaTime;

                isMove = dir.magnitude != 0f;
            }

            animator.SetBool("isMove", isMove);
        }
    }
}
