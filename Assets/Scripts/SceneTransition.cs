using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    static GameManager gameManager;
    [Tooltip("이동하려는 Scene 이름")]
    public string goTo;
    [Tooltip("Scene 이동 후 플레이어 위치")]
    public Vector3 teleportPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.transferScene = goTo;
        gameManager.teleportPosition = teleportPosition;
        Debug.Log(gameManager.transferScene);
        gameManager.GetTransitionAnimator().SetBool("FadeOut", true);
        gameManager.GetTransitionAnimator().SetBool("FadeIn", false);

        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(gameManager.transitionTime);

        StartCoroutine(gameManager.AsyncLoadMap());
        yield return null;
    }
}
