using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    public static bool isStarting = false;
    GameObject[] Hands;
    private void Start()
    {
        Hands = this.gameObject.GetComponentsInChildren<GameObject>();

        for (int i = 0; i < Hands.Length; i++)
            if(Hands[i])
                Hands[i].SetActive(true);
    }
    private void Update()
    {
        if (isStarting)
        {
             StartCoroutine(lerpCoroutine(Hands[2].transform.position, new Vector2(Hands[2].transform.position.x, -1.13f), 1f));
             StartCoroutine(lerpCoroutine(Hands[2].transform.position, new Vector2(Hands[2].transform.position.x, -5f), 1f));           
        }
    }
    IEnumerator lerpCoroutine(Vector2 current, Vector2 target, float time)
    {
        yield return new WaitForSeconds(1f);
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);

            this.transform.position
                = Vector3.Lerp(current, target, elapsedTime / time);

            yield return null;
        }
       
        this.transform.position = target;
        yield return null;
    }
}
