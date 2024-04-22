using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���򺰷� �����ϴ� FloatingCrew�� �����ϴ� Ŭ����
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

    //���� ��, ���� �� 12���� Crew Floater�� ����
    private void Start()
    {
        for(int i = 0; i < (int)EPlayerColor.Count; ++i)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    //Ư�� �ð����� Floating Crew�� ����
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
    /// �������� ���� playerColor ���� dist ���� �Ÿ��� Floating Crew�� �����ϴ� �Լ�
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

            //������ crew�� ȯ�� ����
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
