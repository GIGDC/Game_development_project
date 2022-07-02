using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoardUI : MonoBehaviour
{
    [Tooltip("해당 ui가 어떤 확대된 칠판(magnified)에 대한 것인지 지정")]
    public GameObject magnifiedBoard;
    [Tooltip("미션을 수행하기 위한 아이템 이름")]
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
            boardUI.SetActive(true); // 칠판 이미지 띄울 시 ui도 활성화
            GameObject useCardBtn = boardUI.transform.Find("useCardBtn").gameObject;
            GameObject exitBtn = boardUI.transform.Find("exitBtn").gameObject;
            if (missionItemUsed == false // 미션 아이템 사용 전 && 미션 아이템 소유 중
                && player.GetComponent<PlayerMissionItem>().GetMissionItem(missionItem) != null)
            {
                useCardBtn.SetActive(true);
                useCardBtn.GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(-160, -53, 0);
                exitBtn.GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(160, -53, 0);
            }
            else if (player.GetComponent<PlayerMissionItem>().GetMissionItem(missionItem) == null
                || missionItemUsed == true) // 미션 아이템이 플레이어한테 없거나 이미 사용했을 때
            {
                exitBtn.GetComponent<RectTransform>().anchoredPosition
                    = new Vector3(0, -53, 0); // 닫기 버튼만 표시
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
