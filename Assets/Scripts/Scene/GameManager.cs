using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int test;

    private PlayerAttacted attack;

    private Monster monster;
    PlayerMovement player;

    static public List<string> openDoorList;
    static float mission1Progress; // 미션 1 진행 퍼센트

    public float transitionTime = 1f;
    public string transferScene; // 이동할 씬 이름 (protected: 계단 이동의 경우 1F, 2F, 3F 등이 있으므로 unity editor에서 수정하기 어려움)
    protected Animator transitionAnimator;
    public Vector3 teleportPosition = new Vector3(0, 0, 0); // 플레이어가 씬 이동하고나서의 위치

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

        if (openDoorList == null)
            openDoorList = new List<string>();
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
                Debug.Log("Attack.Zero");
                if (monster != null)
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

    virtual public IEnumerator FadeOut(Vector3 teleportPosition = default(Vector3))
    {
        if(teleportPosition != default(Vector3)) // 0, 0, 0
            this.teleportPosition = teleportPosition;
        transitionAnimator.SetBool("FadeOut", true);
        transitionAnimator.SetBool("FadeIn", false);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(AsyncLoadMap());
        yield return null;
    }

    virtual public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.5f);

        if (teleportPosition != new Vector3(0, 0, 0))
            player.transform.position = teleportPosition;

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
            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;

                /* 
                이 사이에 player의 position을 조정하고 싶은데 맵이 완전히 로딩되어 door을
                찾기 위해 yield return new WaitForSeconds(0.5f)를 넣었으나, 해당 코드가 실행되고
                그 아래의 코드가 실행이 안 되어 player의 position도 못 잡을 뿐더러 FadeIn도 실행되지 않음.
                코루틴의 동작 방식을 내가 완전히 이해하고 있는 게 아닌가 싶음...
                 */

                StartCoroutine(FadeIn());
            }
            yield return null;
        }
    }

    public Animator GetTransitionAnimator()
    {
        return GameObject.Find("GameManager").GetComponent<Animator>();
    }

    public float Mission1Progress
    {
        get { return mission1Progress; }
        set { mission1Progress = value; }
    }
}