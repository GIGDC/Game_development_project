using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveByStairs : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1f;
    public string transferFloor; // 이동할 층

    private void Awake()
    {

        animator = GetComponent<Animator>();
    }

    public void LoadMap()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        animator.SetBool("FadeOut", true);
        animator.SetBool("FadeIn", false);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(AsyncLoadMap());
        yield return null;
    }

    IEnumerator AsyncLoadMap()
    {
        if (GameObject.Find("1F").GetComponent<Button>().onClick != null)
            transferFloor = "1F";
        Debug.Log(transferFloor);
        AsyncOperation async = SceneManager.LoadSceneAsync(transferFloor);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            
            Debug.Log("비동기화 진행도: " + async.progress);

            if (async.progress >= 0.9f)
            {
                StartCoroutine(FadeIn());
                async.allowSceneActivation = true;
                
            }
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        animator.SetBool("FadeOut", false);
        animator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        animator.SetBool("FadeOut", false);
        animator.SetBool("FadeIn", false);
        // transform.parent.parent.parent.gameObject.SetActive(false); // UI 비활성화
        yield return null;
    }
}

