using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorTransfer : MonoBehaviour
{
    GameManager gameManager;

    Animator DoorAni;
    [Tooltip("이동하려는 Scene 이름")]
    public string GoTo;
    public Vector3 teleportPosition = new Vector3(0, 0, 0); // 플레이어가 씬 이동하고나서의 위치
    StartPoint start;
    KeyController key;
    public Image WarningUI;
    // Start is called before the first frame update
    //PlayerMovement player;
    CameraShake shake;

    public bool isOpeningDoor; // 문과 부딪혔을때에는 부적이 아닌 문을 열수 있도록 하기 위함.

    static public bool CheckMonster = false;

    [Tooltip("원래 열려있는 문임")]
    public bool isOpen = false;

    void Start()
    {
        start = GameObject.FindObjectOfType<StartPoint>();
        key = GameObject.FindObjectOfType<KeyController>();
        DoorAni = this.GetComponent<Animator>();

        //player= GameObject.FindObjectOfType<PlayerMovement>();
        shake = GameObject.FindObjectOfType<CameraShake>();

        gameManager = GameObject.FindObjectOfType<GameManager>();
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
            isOpeningDoor = true;
            string sceneName = this.gameObject.name.Split(' ')[0];
            Debug.Log("sceneName: " + sceneName + ", list: " + GameManager.openDoorList[0]);
            if (GameManager.openDoorList.Contains(sceneName))
            {
                isOpen = true;
                isOpeningDoor = true;
            }

            if (KeyController.isLock || isOpen)
            {

                SceneTransition();
            }
            else if (WarningUI != null)
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
        DoorAni.SetBool("isOpening", true);
        gameManager.transferScene = GoTo;
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