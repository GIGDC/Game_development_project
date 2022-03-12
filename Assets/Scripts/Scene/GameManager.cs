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
    public string transferScene; // 이동할 씬 이름 (protected: 계단 이동의 경우 1F, 2F, 3F 등이 있으므로 unity editor에서 수정하기 어려움)
    protected Animator transitionAnimator;

    private void Awake()
    {
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManagers.Length == 1) 
        { 
            DontDestroyOnLoad(gameObject); 
        } 
        else 
        { 
            Destroy(gameObject);
        } // 중복된 GameMangager 오브젝트가 있을 경우 오브젝트 파괴

        transitionAnimator = GetComponent<Animator>();

        GameObject transition = transform.Find("UI").Find("Transition").gameObject;
        transition.SetActive(true);
    }

    private void Start()
    {
        attack = GameObject.FindObjectOfType<PlayerAttacted>(); // Timer_60에 대한 clock을 찾음
        monster = GameObject.FindObjectOfType<Monster>();
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (transferScene != null)
        {
            PlayerMovement.CurrentMapName = transferScene;
        }
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
        if (transferMapName != null)
        {
            PlayerMovement.CurrentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }
    public IEnumerator LoadMap()
    {
        if (transferScene != null)
        {
           
            yield return new WaitForSeconds(0f);
            SceneManager.LoadScene(transferScene);
        }
    }

    virtual public IEnumerator FadeOut()
    {
        transitionAnimator.SetBool("FadeOut", true);
        transitionAnimator.SetBool("FadeIn", false);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(AsyncLoadMap());
        yield return null;
    }

    virtual public IEnumerator FadeIn()
    {
        transitionAnimator.SetBool("FadeOut", false);
        transitionAnimator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        transitionAnimator.SetBool("FadeOut", false);
        transitionAnimator.SetBool("FadeIn", false);
        yield return null;
    }

    virtual public IEnumerator AsyncLoadMap()
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

    public Animator GetTransitionAnimator()
    {
        return GameObject.Find("GameManager").GetComponent<Animator>();
    }
}