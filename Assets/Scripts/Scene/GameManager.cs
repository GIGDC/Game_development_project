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
            Debug.Log("Attack.Zero");
            if(monster!=null)
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

    virtual public IEnumerator FadeIn(bool isDoorBelowPlayer = false, string currentScene = "", string direction = "")
    {
        yield return new WaitForSeconds(0.5f); // 맵이 완전히 로딩되어 원하는 door을 찾을 수 있도록 0.5초 대기

        Vector3 teleportLocation = player.transform.position; // 초기화
        if (currentScene != "") // 현재 Scene의 이름 == 새로운 Scene에서 player가 들어오는 door 이름
        {
            if (direction != "") // 문의 위치 (back/front)
                teleportLocation = GameObject.Find("Door").transform.Find(currentScene + " Door (" + direction + ")").transform.position;
            else
                teleportLocation = GameObject.Find("Door").transform.Find(currentScene).transform.position;
        }
        if (isDoorBelowPlayer) // door과 player의 상대적인 위치
        {
            // 새로운 scene에서 player과 door의 상대적인 위치 설정
            player.transform.position = new Vector3(teleportLocation.x, teleportLocation.y - 3, teleportLocation.z);
        }
        else
        {
            player.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + 3, teleportLocation.z);
        }

        transitionAnimator.SetBool("FadeOut", false);
        transitionAnimator.SetBool("FadeIn", true);

        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        transitionAnimator.SetBool("FadeOut", false);
        transitionAnimator.SetBool("FadeIn", false);
        yield return null;
    }

    virtual public IEnumerator AsyncLoadMap(bool isDoorBelowPlayer = false, string currentScene = "", string direction = "")
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(transferScene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {

            Debug.Log("비동기화 진행도: " + async.progress);

            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
                
                /* 
                이 사이에 player의 position을 조정하고 싶은데 맵이 완전히 로딩되어 door을
                찾기 위해 yield return new WaitForSeconds(0.5f)를 넣었으나, 해당 코드가 실행되고
                그 아래의 코드가 실행이 안 되어 player의 position도 못 잡을 뿐더러 FadeIn도 실행되지 않음.
                코루틴의 동작 방식을 내가 완전히 이해하고 있는 게 아닌가 싶음...
                 */

                StartCoroutine(FadeIn(isDoorBelowPlayer, currentScene, direction));
            }
            yield return null;
        }
    }

    public Animator GetTransitionAnimator()
    {
        return GameObject.Find("GameManager").GetComponent<Animator>();
    }
}