using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEdBoardJumpScare : MonoBehaviour
{
    public Image monsterJumpScare;
    public Image boardUI;
    Button exitBtn;
    [Tooltip("현재 진행하고 있는 미션 번호")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("미션 진행 상황이 몇 %일 때 이벤트를 active할 것인지 지정")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("작업 완료 시 미션 진행 상황 설정")]
    [SerializeField] float missionProgress;

    private void Start()
    {
        exitBtn = boardUI.transform.Find("exitBtn").GetComponent<Button>();
        exitBtn.onClick.AddListener(OnExitBtnClick);
    }

    void OnExitBtnClick()
    {
        if (GameObject.FindObjectOfType<GameManager>().Mission1Progress != activeByMissionProgress)
            return;

        if (this.GetComponent<ActivateBoardUI>().MissionItemUsed)
        {
            monsterJumpScare.gameObject.SetActive(true);
            GameObject.FindObjectOfType<GameManager>().Mission1Progress = missionProgress;
        }
    }
}
