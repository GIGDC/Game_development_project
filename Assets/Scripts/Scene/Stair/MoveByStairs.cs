using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveByStairs : MonoBehaviour
{
    public Animator sceneTransition;
    public float transitionTime = 1f;
    public string transferMapName; // �̵��� ���� �̸�

    public void LoadMap()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        sceneTransition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(AsyncLoadMap());
        yield return null;
    }

    IEnumerator AsyncLoadMap()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(transferMapName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            Debug.Log("�񵿱�ȭ ���൵: " + async.progress + "%");

            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
                transform.parent.parent.parent.gameObject.SetActive(false); // UI ��Ȱ��ȭ
            }
            yield return null;
        }
    }
}

