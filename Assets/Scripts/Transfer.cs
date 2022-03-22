using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transfer : MonoBehaviour
{
    GameManager gameManager;
    Animator doorAnimator;
    [Tooltip("이동하려는 Scene 이름")]
    public string GoTo;
    [Tooltip("문의 위치 front / back")]
    public string direction;
    StartPoint start;
    KeyController key;
    public Image WarningUI;
    // Start is called before the first frame update
    PlayerMovement player;
    CameraShake shake;

    static public bool CheckMonster = false;

    [Tooltip("개발용 변수 (열쇠가 없어도 문을 열 수 있음)")]
    public bool dontCheckKeyController = false; // 개발할 때 열쇠 없어도 문으로 쉽게 이동 가능하도록

    void Start()
    {
        start = GameObject.FindObjectOfType<StartPoint>();
        key = GameObject.FindObjectOfType<KeyController>();
        doorAnimator = GetComponent<Animator>();
        player= GameObject.FindObjectOfType<PlayerMovement>();
        shake = GameObject.FindObjectOfType<CameraShake>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && !MonsterTimer.OutDoor)
        {
            CheckMonster = true;
        }

        if (collision.gameObject.name != "Player")
            return; //여기에 object 숨겨진 후 몇초 뒤에 등장하도록 구현 (ex: 몹)
                    // manager.transferScene = "SampleScene";

        StartPoint.MapNum = SceneManager.GetActiveScene().buildIndex;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.isDoor = true;
            if (KeyController.isLock || dontCheckKeyController) // 추후 dontCheckKeyController만 조건에서 삭제
            {
                if (direction != "")
                {
                    StartPoint.direction = direction;
                }
                doorAnimator.SetTrigger("OpenDoor");
                SceneTransition();
            }
         else if(WarningUI != null)
        {
                WarningUI.gameObject.SetActive(true);
                shake.Shake();
            }
        }

    }

    public void SceneTransition()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.transferScene = GoTo;
        Debug.Log(gameManager.transferScene);
        gameManager.GetTransitionAnimator().SetBool("FadeOut", true);
        gameManager.GetTransitionAnimator().SetBool("FadeIn", false);
        //doorAnimator.SetTrigger("OpenDoor");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(gameManager.transitionTime);
        StartCoroutine(gameManager.AsyncLoadMap());
        yield return null;
    }
}