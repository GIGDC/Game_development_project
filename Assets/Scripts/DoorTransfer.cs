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

    public bool isOpeningDoor; // ���� �ε����������� ������ �ƴ� ���� ���� �ֵ��� �ϱ� ����.

    static public bool CheckMonster = false;

    [Tooltip("���� �����ִ� ����")]
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
            return; //���⿡ object ������ �� ���� �ڿ� �����ϵ��� ���� (ex: ��)
                    // manager.transferScene = "SampleScene";

        StartPoint.MapNum = SceneManager.GetActiveScene().buildIndex;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            string sceneName = this.gameObject.name.Split(' ')[0]; // ���� ����� �� �̸�
            if (GameManager.openDoorList.Contains(sceneName)) // �ش� ���� �����ִ��� üũ
            {
                Debug.Log(sceneName + " �� ����");
                isOpen = true;
            }

            if (KeyController.isLock || isOpen) // �������� �� Ʈ������
            {
                isOpeningDoor = true;
                SceneTransition();
            }
            else if (WarningUI != null) // �������� �� ��� ui
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
}