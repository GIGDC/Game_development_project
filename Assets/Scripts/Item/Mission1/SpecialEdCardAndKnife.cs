using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEdCardAndKnife : MonoBehaviour
{
    GameObject player;
    [SerializeField] Image cardImage;
    [SerializeField] Image knifeImage;
    GameObject target; // ���콺�� ����Ű�� ���
    [SerializeField] GameObject glass;
    bool isCollider = false;
    [Tooltip("Ŀ��Į gameObject ����")]
    [SerializeField] GameObject knife;
    [Tooltip("������ ȹ�� �� �ʿ��� �������� ���̰� �� ������(default: false)")]
    [SerializeField] bool activeAfterObtaining = false;
    [Tooltip("PlayerMissionItem ��ũ��Ʈ �� missionItem�� ������ �̸� ����")]
    [SerializeField] string missionItemName;
    bool isCardCut; // ī�尡 �߶��� ��������

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
        isCardCut = false;
        if(player.GetComponent<PlayerMissionItem>().GetMissionItem("mission1_card") == null)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (cardImage.gameObject.activeSelf)
        {
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
                    knife.SetActive(activeAfterObtaining);
                    player.GetComponent<PlayerMissionItem>().AddMissionItem(missionItemName); // ȹ���� �̼� ������ ����Ʈ�� �߰�
                }
                cardImage.gameObject.SetActive(false);
                knifeImage.gameObject.SetActive(false);
                player.GetComponent<PlayerMovement>().CanPlayerMove = true;
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
