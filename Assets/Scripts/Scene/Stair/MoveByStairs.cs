using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveByStairs : MonoBehaviour
{
    public Animator sceneTransition;
    public float transitionTime = 1f;
    public string transferMapName; // �̵��� ���� �̸�

    public void ChangeScene()
    {
        Debug.Log("�� �̵�");
        StartCoroutine(LoadMap(transferMapName));
    }

    IEnumerator LoadMap(string transferMapName)
    {
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation async = SceneManager.LoadSceneAsync(transferMapName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            Debug.Log("�񵿱�ȭ ���൵: " + async.progress + "%");

            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
                transform.parent.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}

