using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerAttacted attack;

    private Monster monster;
    PlayerMovement player;

    public float transitionTime = 1f;
    public string transferScene; // �̵��� �� �̸� (protected: ��� �̵��� ��� 1F, 2F, 3F ���� �����Ƿ� unity editor���� �����ϱ� �����)
    protected Animator transitionAnimator;

    private void Awake()
    {
        transitionAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        attack = GameObject.FindObjectOfType<PlayerAttacted>(); // Timer_60�� ���� clock�� ã��
        monster = GameObject.FindObjectOfType<Monster>();
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {   
        if (Timer_60.isStop || attack.zeroHP)
        {
            monster.Hide();
            player.Hide();

            StartCoroutine(LoadMap("GameOver"));
        }
    }

    public IEnumerator LoadMap(string transferMapName)
    {
        Timer_60.isStop = false;
        yield return new WaitForSeconds(0f);

        player.CurrentMapName = transferMapName;
        SceneManager.LoadScene(transferMapName);
    }
    public IEnumerator LoadMap()
    {

        if (transferScene != null)
        {
            player.CurrentMapName = transferScene;
            yield return new WaitForSeconds(0f);
            SceneManager.LoadScene(transferScene);
        }
    }

    virtual protected IEnumerator FadeOut()
    {
        transitionAnimator.SetBool("FadeOut", true);
        transitionAnimator.SetBool("FadeIn", false);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(AsyncLoadMap());
        yield return null;
    }

    virtual protected IEnumerator FadeIn()
    {
        transitionAnimator.SetBool("FadeOut", false);
        transitionAnimator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        transitionAnimator.SetBool("FadeOut", false);
        transitionAnimator.SetBool("FadeIn", false);
        yield return null;
    }

    protected IEnumerator AsyncLoadMap()
    {
        player.CurrentMapName = transferScene;
        AsyncOperation async = SceneManager.LoadSceneAsync(transferScene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {

            Debug.Log("�񵿱�ȭ ���൵: " + async.progress);

            if (async.progress >= 0.9f)
            {
                StartCoroutine(FadeIn());
                async.allowSceneActivation = true;

            }
            yield return null;
        }
    }

    protected Animator GetTransitionAnimator()
    {
        return GameObject.Find("GameManager").GetComponent<Animator>();
    }
}
