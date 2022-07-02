using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorTransfer : MonoBehaviour
{
    GameManager gameManager;

    Animator DoorAni;
    [Tooltip("�̵��Ϸ��� Scene �̸�")]
    public string GoTo;
    public Vector3 teleportPosition = new Vector3(0, 0, 0); // �÷��̾ �� �̵��ϰ����� ��ġ
    StartPoint start;
    KeyController key;
    public Image WarningUI;
    // Start is called before the first frame update
    //PlayerMovement player;
    CameraShake shake;
    public Image SuccessUI;
    GameObject key_option;

    public bool isOpeningDoor; // ���� �ε����������� ������ �ƴ� ���� ���� �ֵ��� �ϱ� ����.

    static public bool CheckMonster = false;

    [Tooltip("���߿� ���� (���谡 ��� ���� �� �� ����)")]
    public bool dontCheckKeyController = false; // ������ �� ���� ��� ������ ���� �̵� �����ϵ���

    void Start()
    {
        key_option = GameObject.Find("����ڹ���");
        start = GameObject.FindObjectOfType<StartPoint>();
        key = GameObject.FindObjectOfType<KeyController>();
        DoorAni = this.GetComponent<Animator>();
        
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

        if (LockController.isLock)
        {
            key_option.gameObject.SetActive(false);
            StartCoroutine(deleayTime());
            SuccessUI.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOpeningDoor = true;
            if (LockController.isLock) // ���� dontCheckKeyController�� ���ǿ��� ����
            {
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
        DoorAni.SetBool("isOpening", true);
        gameManager = GameObject.FindObjectOfType<GameManager>();
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
    IEnumerator deleayTime()
    {
        SuccessUI.gameObject.SetActive(true);
        Debug.Log("���� ���Ƚ��ϴ�.");
        yield return new WaitForSeconds(5);
    }
}