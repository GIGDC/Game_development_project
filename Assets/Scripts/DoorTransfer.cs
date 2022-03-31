using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorTransfer : MonoBehaviour
{
    GameManager gameManager;
    public Animator doorAnimator;
    static int doorAnimCnt;
    [Tooltip("�̵��Ϸ��� Scene �̸�")]
    public string GoTo;
    [Tooltip("���� ��ġ front / back")]
    public string direction;
    StartPoint start;
    KeyController key;
    public Image WarningUI;
    // Start is called before the first frame update
    //PlayerMovement player;
    CameraShake shake;

    public bool isOpeningDoor; // ���� �ε����������� ������ �ƴ� ���� ���� �ֵ��� �ϱ� ����.

    static public bool CheckMonster = false;

    [Tooltip("���߿� ���� (���谡 ��� ���� �� �� ����)")]
    public bool dontCheckKeyController = false; // ������ �� ���� ��� ������ ���� �̵� �����ϵ���

    void Start()
    {
        start = GameObject.FindObjectOfType<StartPoint>();
        key = GameObject.FindObjectOfType<KeyController>();
        doorAnimator = GetComponent<Animator>();
        //player= GameObject.FindObjectOfType<PlayerMovement>();
        shake = GameObject.FindObjectOfType<CameraShake>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && !MonsterTimer.OutDoor)
        {
            CheckMonster = true;
        }

        if (collision.gameObject.name != "Player")
            return; //���⿡ object ������ �� ���� �ڿ� �����ϵ��� ���� (ex: ��)
                    // manager.transferScene = "SampleScene";

        StartPoint.MapNum = SceneManager.GetActiveScene().buildIndex;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOpeningDoor = true;
            if (KeyController.isLock || dontCheckKeyController) // ���� dontCheckKeyController�� ���ǿ��� ����
            {
                //if (direction != "")
                //{
                //    StartPoint.direction = direction;
                //}
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

        bool isDoorBelowPlayer = GameObject.Find("Player").transform.position.y > transform.position.y;
        StartCoroutine(gameManager.AsyncLoadMap(
            isDoorBelowPlayer,
            SceneManager.GetActiveScene().name,
            direction));
        yield return null;
    }
}