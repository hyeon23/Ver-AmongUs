using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBody : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> steams = new List<GameObject>();
    [SerializeField]
    private List<SpriteRenderer> sparks = new List<SpriteRenderer>();
    [SerializeField]
    private List<Sprite> sparkSprites = new List<Sprite>();

    private int nowIdx = 0;

    private void Start()
    {
        foreach(var steam in steams)
        {
            StartCoroutine(RandomSteam(steam));
        }
        StartCoroutine(SparkEngine());
    }

    private IEnumerator RandomSteam(GameObject steam)
    {
        while (true)
        {
            float timer = Random.Range(0.5f, 1.5f);
            while(timer >= 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }
            steam.SetActive(true);
            timer = 0;
            while(timer <= 0.6f)
            {
                yield return null;
                timer += Time.deltaTime;
            }
            steam.SetActive(false);
        }
    }

    private IEnumerator SparkEngine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        while (true)
        {
            float timer = Random.Range(0.2f, 1.5f);
            while(timer >= 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }
            int[] indices = new int[Random.Range(2, 7)];
            for(int i = 0; i< indices.Length; ++i)
            {
                indices[i] = Random.Range(0, sparkSprites.Count);
            }
            for(int i = 0; i < indices.Length; ++i)
            {
                yield return wait;
                sparks[nowIdx].sprite = sparkSprites[indices[i]];
            }
            sparks[nowIdx++].sprite = null;
            if(nowIdx >= sparks.Count)
            {
                nowIdx = 0;
            }
        }
    }
}
