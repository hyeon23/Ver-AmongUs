using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPipeLight : MonoBehaviour
{
    private Animator anime;
    private WaitForSeconds wait0_15 = new WaitForSeconds(0.15f);

    private List<WeaponPipeLight> lights = new List<WeaponPipeLight>();

    private void Start()
    {
        anime = GetComponent<Animator>();
        for(int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i).GetComponent<WeaponPipeLight>();
            if(child != null)
            {
                lights.Add(child);
            }
        }
    }

    public void TurnOnLight()
    {
        anime.SetTrigger("on");
        StartCoroutine(TrunOnLightAtChild());
    }

    private IEnumerator TrunOnLightAtChild()
    {
        yield return wait0_15;

        foreach(var child in lights)
        {
            child.TurnOnLight();
        }
    }
}
