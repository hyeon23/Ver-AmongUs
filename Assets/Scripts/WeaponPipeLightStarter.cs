using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPipeLightStarter : MonoBehaviour
{
    private WaitForSeconds wait2 = new WaitForSeconds(2f);

    private List<WeaponPipeLight> lights = new List<WeaponPipeLight>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i).GetComponent<WeaponPipeLight>();
            if(child != null)
            {
                lights.Add(child);
            }
        }
        StartCoroutine(TurnOnPipeLight());
    }

    private IEnumerator TurnOnPipeLight()
    {
        //재귀적으로 모든 자식 불빛들이 퍼저나가는 현상
        while (true)
        {
            yield return wait2;

            foreach(var child in lights)
            {
                child.TurnOnLight();
            }
        }
    }
}
