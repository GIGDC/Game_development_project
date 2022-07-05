using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEdBoardJumpScare : MonoBehaviour
{
    public Image monsterJumpScare;
    public Image boardUI;
    Button exitBtn;
    [Tooltip("���� �����ϰ� �ִ� �̼� ��ȣ")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("�̼� ���� ��Ȳ�� �� %�� �� �̺�Ʈ�� active�� ������ ����")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("�۾� �Ϸ� �� �̼� ���� ��Ȳ ����")]
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
