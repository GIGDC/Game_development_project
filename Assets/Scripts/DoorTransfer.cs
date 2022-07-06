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
    GameObject key_option;

    public bool isOpeningDoor; // 문과 부딪혔을때에는 부적이 아닌 문을 열수 있도록 하기 위함.

    static public bool CheckMonster = false;

    [Tooltip("원래 열려있는 문임")]
    public bool isOpen = false;

    void Start()
    {
        LockController.isLock = false;
        key_option = GameObject.Find("잠긴자물쇠");
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

        if (LockController.isLock)
        {
            if (key_option)
                key_option.gameObject.SetActive(false);
            StartCoroutine(deleayTime());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            string sceneName = this.gameObject.name.Split(' ')[0]; // 문과 연결된 씬 이름
            if (GameManager.openDoorList.Contains(sceneName)) // 해당 문이 열려있는지 체크
            {
                Debug.Log(sceneName + " 문 열림");
                isOpen = true;
            }

            if (isOpen || LockController.isLock) // 열려있을 시 트랜지션
            {
                isOpeningDoor = true;
                SceneTransition();
            }
            else if (WarningUI != null) // 닫혀있을 시 경고 ui
            {
                WarningUI.gameObject.SetActive(true);
                shake.Shake();
            }
        }

    }

    private void Update()
    {
        if (isOpeningDoor)
            DoorAni.SetBool("isOpening", true);
        if (isOpen)
            DoorAni.SetBool("isOpen", true);
    }

    public void SceneTransition()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.transferScene = GoTo;
        gameManager.teleportPosition = teleportPosition;
        StartCoroutine(gameManager.FadeOut());
    }
    IEnumerator deleayTime()
    {
        Debug.Log("문이 열렸습니다.");
        yield return new WaitForSeconds(5);
    }
}