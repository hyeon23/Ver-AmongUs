using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    ///높이에 따른 Sprite 정렬 기능
    //-Back과 Front의 값을 여유를 줘서 설정
    //-절대 값을 계산하므로, GameObject의 좌표가 Back or Front를 상회하는 경우 문제 발생
    [SerializeField]
    private Transform Back;
    [SerializeField]
    private Transform Front;

    /// <summary>
    /// objDist: back과 go간 거리
    /// totalDist: back과 front의 거리
    /// 16bit 최대 값 사이 중 현재 거리 / 최대 거리의 비율에 따른 값을 리턴
    /// </summary>
    /// <param name="go">SortingOrder의 값을 변경하고 싶은 오브젝트</param>
    /// <returns>Int16.MinValue-Int16.MaxValue 중 퍼센트에 맞는 SortingOrder 값 리턴</returns>
    public int GetSortingOrder(GameObject go)
    {
        float objDist = Mathf.Abs(Back.position.y - go.transform.position.y);
        float totalDist = Mathf.Abs(Back.position.y - Front.position.y);

        return (int)(Mathf.Lerp(System.Int16.MinValue, System.Int16.MaxValue, objDist / totalDist));
    }
}
