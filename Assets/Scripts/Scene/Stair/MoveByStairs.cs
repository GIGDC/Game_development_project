using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveByStairs : MonoBehaviour
{
    public Animator sceneTransition;
    public float transitionTime = 1f;

    public string transferMapName; // 이동할 맵의 이름

    Button button;

    private void Awake()
    {
        button = this.transform.GetComponent<Button>();
    }

    public void ChangeScene()
    {
        StartCoroutine(LoadMap(transferMapName));
    }

    IEnumerator LoadMap(string transferMapName)
    {
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(transferMapName);
    }
}

