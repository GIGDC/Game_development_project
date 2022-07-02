using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoardUI : MonoBehaviour
{
    [Tooltip("�ش� ui�� � Ȯ��� ĥ��(magnified)�� ���� ������ ����")]
    public GameObject magnifiedBoard;
    [Tooltip("�̼��� �����ϱ� ���� ������ �̸�")]
    public string missionItem;
    public GameObject UI;
    GameObject player;
    GameObject boardUI;
    bool missionItemUsed = false;

    void Start()
    {
        player = GameObject.Find("Player");
        boardUI = transform.Find("boardUI").gameObject;
        boardUI.SetActive(false);
    }


    void Update()
    {
        if (magnifiedBoard.activeSelf == true)
        {
            UI.SetActive(false);
            boardUI.SetActive(true); // ĥ�� �̹��� ��� �� ui�� Ȱ��ȭ
            GameObject useCardBtn = boardUI.transform.Find("useCardBtn").gameObject;
            GameObject exitBtn = boardUI.transform.Find("exitBtn").gameObject;
            if (missionItemUsed == false // �̼� ������ ��� �� && �̼� ������ ���� ��
                && player.GetComponent<PlayerMissionItem>().GetMissionItem(missionItem) != null)
            {
                useCardBtn.SetActive(true);
                useCardBtn.GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(-160, -53, 0);
                exitBtn.GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(160, -53, 0);
            }
            else if (player.GetComponent<PlayerMissionItem>().GetMissionItem(missionItem) == null
                || missionItemUsed == true) // �̼� �������� �÷��̾����� ���ų� �̹� ������� ��
            {
                exitBtn.GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(0, -53, 0); // �ݱ� ��ư�� ǥ��
                useCardBtn.SetActive(false);
            }
        }
        else
            boardUI.SetActive(false);
    }

    public void SetMissionItemUsed(bool b)
    {
        missionItemUsed = b;
    }
}
