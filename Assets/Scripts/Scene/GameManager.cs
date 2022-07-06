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
    static float mission1Progress; // �̼� 1 ���� �ۼ�Ʈ

    public float transitionTime = 1f;
    public string transferScene; // �̵��� �� �̸� (protected: ��� �̵��� ��� 1F, 2F, 3F ���� �����Ƿ� unity editor���� �����ϱ� �����)
    protected Animator transitionAnimator;
    public Vector3 teleportPosition = new Vector3(0, 0, 0); // �÷��̾ �� �̵��ϰ����� ��ġ

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
        } // �ߺ��� GameMangager ������Ʈ�� ���� ��� ������Ʈ �ı�

        transitionAnimator = GetComponent<Animator>();

        GameObject transition = transform.Find("UI").Find("Transition").gameObject;
        transition.SetActive(true);

        if (openDoorList == null)
            openDoorList = new List<string>();
    }

    private void Start()
    {
        attack = GameObject.FindObjectOfType<PlayerAttacted>(); // Timer_60�� ���� clock�� ã��
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
                �� ���̿� player�� position�� �����ϰ� ������ ���� ������ �ε��Ǿ� door��
                ã�� ���� yield return new WaitForSeconds(0.5f)�� �־�����, �ش� �ڵ尡 ����ǰ�
                �� �Ʒ��� �ڵ尡 ������ �� �Ǿ� player�� position�� �� ���� �Ӵ��� FadeIn�� ������� ����.
                �ڷ�ƾ�� ���� ����� ���� ������ �����ϰ� �ִ� �� �ƴѰ� ����...
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