using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEdCard : MonoBehaviour
{
    GameObject player;
    public Image cardImage;
    public Image knifeImage;
    GameObject target; // ���콺�� ����Ű�� ���
    public GameObject glass;
    [Tooltip("ActivateBoardUI ��ũ��Ʈ�� �ִ� GameObject(ī�� Ŭ�� �� ���� ĥ�ǵ� ���� Ŭ���Ǿ� ui�� ��Ÿ���Ƿ� ���� ����)")]
    public GameObject gameObjectWithActivateBoardUI;
    [Tooltip("ClickController ��ũ��Ʈ�� �ִ� GameObject(ī�� Ŭ�� �� ���� ĥ�ǵ� ���� Ŭ���Ǿ� ui�� ��Ÿ���Ƿ� ���� ����)")]
    public GameObject gameObjectWithClickController;
    bool isCollider = false;
    [Tooltip("������ ȹ�� �� �ʿ��� �������� ���̰� �� ������(default: false)")]
    public bool activeAfterObtaining = false;
    bool isCardCut; // ī�尡 �߶��� ��������

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
        isCardCut = false;
        if(player.GetComponent<PlayerMissionItem>().GetMissionItem(this.gameObject.name) != null)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (cardImage.gameObject.activeSelf)
        {
            gameObjectWithActivateBoardUI.GetComponent<ActivateBoardUI>().enabled = false;
            gameObjectWithClickController.GetComponent<ClickController>().enabled = false;
            if(!isCardCut && player.GetComponent<PlayerMissionItem>().GetMissionItem("Ŀ��Į") != null) // �÷��̾ Ŀ��Į ���� �� ȭ�鿡 �̹��� ǥ��
            {
                knifeImage.gameObject.SetActive(true);
            }
            else
            {
                knifeImage.gameObject.SetActive(false);
            }

            if (isCardCut) // ī�尡 �߶����� �߶��� ī�� �̹��� ǥ��
            {
                cardImage.GetComponent<Animator>().SetTrigger("isCut");
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                if(!isCardCut) // ī�尡 �߶����� ���� ���¸� �÷��̾ ����� ī�� ȹ�� �Ұ�
                {
                    this.gameObject.SetActive(true);
                }
                else
                {
                    this.gameObject.SetActive(activeAfterObtaining);
                    player.GetComponent<PlayerMissionItem>().AddMissionItem(this.gameObject.name); // ȹ���� �̼� ������ ����Ʈ�� �߰�
                }
                cardImage.gameObject.SetActive(false);
                player.GetComponent<PlayerMovement>().CanPlayerMove = true;
                gameObjectWithActivateBoardUI.GetComponent<ActivateBoardUI>().enabled = true;
                gameObjectWithClickController.GetComponent<ClickController>().enabled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        CastRay();
        if (isCollider && target.CompareTag("magnifiedObj"))
        {
            glass.SetActive(true);
            glass.transform.position = Input.mousePosition;
            isCollider = false;
        }
        else
        {
            glass.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (target == this.gameObject)
            {
                cardImage.gameObject.SetActive(true);
                glass.SetActive(false);
                player.GetComponent<PlayerMovement>().CanPlayerMove = false;
            }
        }
    }

    public void CastRay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            isCollider = true;
        }
    }

    public bool IsCardCut
    {
        get { return isCardCut; }
        set { isCardCut = value; }
    }
}
