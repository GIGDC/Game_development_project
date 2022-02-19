using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Timer_60 clock;
    private Monster monster;
    private PlayerMovement player;

    public float transitionTime = 1f;
    protected string transferScene; // 이동할 씬 이름 (protected: 계단 이동의 경우 1F, 2F, 3F 등이 있으므로 unity editor에서 수정하기 어려움)
    protected Animator animator;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); // memory leak

        animator = GetComponent<Animator>();

        clock = GameObject.FindObjectOfType<Timer_60>(); // Timer_60에 대한 clock을 찾음
        monster = GameObject.FindObjectOfType<Monster>();
        player = GameObject.FindObjectOfType<PlayerMovement>();

    }

    private void Update()
    {   
        if (clock.isStop) // 계단 메뉴가 활성화되었을 때 NullReferenceException이 뜨는 이유는?
        {
            monster.Hide();
            player.Hide();

            StartCoroutine(LoadMap("GameOver"));
        }   
    }

    protected IEnumerator LoadMap(string transferMapName)
    {
        clock.isStop = false;
        yield return new WaitForSeconds(0f);

        SceneManager.LoadScene(transferMapName);
    }


    virtual protected IEnumerator FadeOut()
    {
        animator.SetBool("FadeOut", true);
        animator.SetBool("FadeIn", false);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(AsyncLoadMap());
        yield return null;
    }

    virtual protected IEnumerator FadeIn()
    {
        animator.SetBool("FadeOut", false);
        animator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        animator.SetBool("FadeOut", false);
        animator.SetBool("FadeIn", false);
        yield return null;
    }

    protected IEnumerator AsyncLoadMap()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(transferScene);
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

    protected Animator GetAnimator()
    {
        return animator;
    }
}
