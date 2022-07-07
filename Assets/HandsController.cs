using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    public static bool isStarting = false;
    public GameObject[] Hands;
    bool count = false;
    float elapsedTime;
    private void Start()
    {
        elapsedTime = 0.0f;
        count = true;
        //isStarting으로 변경
        if (ThreeConversation.GhostNum >= 3)
            for (int i = 0; i < Hands.Length; i++)
                if (Hands[i])
                    Hands[i].SetActive(true);

        InvokeRepeating("SecUP", 1f, 1f);

    }
    private void SecUP()
    {
         if (ThreeConversation.GhostNum >= 3)
        {
            StartCoroutine(lerpCoroutine(Hands[0].transform.position, new Vector2(119f, Hands[0].transform.position.y), new Vector2(132.7f, Hands[0].transform.position.y), 1f,0));
            StartCoroutine(lerpCoroutine(Hands[1].transform.position, new Vector2(Hands[1].transform.position.x, 1.5f), new Vector2(Hands[1].transform.position.x, 3f), 1f, 1));
            StartCoroutine(lerpCoroutine(Hands[2].transform.position, new Vector2(Hands[2].transform.position.x, -1.6f), new Vector2(Hands[2].transform.position.x, -5f), 1f, 2));
        }
    }
    bool isCheck = false;
    IEnumerator lerpCoroutine(Vector2 current, Vector2 Up, Vector2 Down, float time, int i)
    {
        elapsedTime = 0.0f;
        if (!isCheck) {
            while (elapsedTime < time)
            {
                elapsedTime += (Time.deltaTime);

                Hands[i].transform.position
                    = Vector3.Lerp(current, Up, elapsedTime / time);

                yield return null;
            }
            Hands[i].transform.position = Up;
        
            if (elapsedTime >= time) 
                isCheck = true;
            yield return null;
        }
        if (isCheck)
        {
            while (elapsedTime > 0.0f)
            {
                elapsedTime -= (Time.deltaTime);

                Hands[i].transform.position
                    = Vector3.Lerp(current, Down, elapsedTime / time);

                yield return null;
            }
            Hands[i].transform.position = Down;

            if (elapsedTime <= 0.0f)
                isCheck = false;
        }
        yield return null;
    }


}
