using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 색깔별로 존재하는 FloatingCrew를 관리하는 클래스
/// </summary>
public class CrewFloater : MonoBehaviour
{
    [SerializeField]
    private GameObject crewPrefab;
    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private bool[] crewStates = new bool[12];
    private float timer = 0.5f;
    private float distance = 10;

    //시작 시, 색깔 별 12개의 Crew Floater를 스폰
    private void Start()
    {
        for(int i = 0; i < (int)EPlayerColor.Count; ++i)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    //특정 시간마다 Floating Crew를 스폰
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    /// <summary>
    /// 생성되지 않은 playerColor 색의 dist 원형 거리에 Floating Crew를 스폰하는 함수
    /// 
    /// </summary>
    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        if (!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;

            float angle = Random.Range(0f, 360f);
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * distance;
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            float floatingSpeed = Random.Range(1f, 4f);
            float rotateSpeed = Random.Range(-3f, 3f);
            var crew = Instantiate(crewPrefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>();

            //생성한 crew의 환경 설정
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction, floatingSpeed, rotateSpeed, Random.Range(0.5f, 1f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FloatingCrew>();
        if(crew != null)
        {
            crewStates[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }
}
