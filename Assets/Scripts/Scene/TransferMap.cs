using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public string transferMapName; // 이동할 맵의 이름
    public Animator doorAnim;
    public int doorCnt;
    [Tooltip("방향 설정")]
    public string direction;
    private Vector2 vector;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(LoadMap(transferMapName));
        }
    }

    IEnumerator LoadMap(string transferMapName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(transferMapName);
    }
}
