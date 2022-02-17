using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveByStairs : GameManager
{
    int cnt = 0;
    private void Start()
    {
        animator = GetAnimator();
    }

    private void Update()
    {
        if (GameObject.Find("1F").GetComponent<Button>().onClick != null && cnt == 0)
        {
            transferScene = "1F";
            StartCoroutine(FadeOut());
            cnt++;
        }
    }

    protected IEnumerator FadeOut()
    {
        animator.SetBool("FadeOut", true);
        animator.SetBool("FadeIn", false);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(AsyncLoadMap());
        yield return null;
    }
}

