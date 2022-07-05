using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInSpecialEdRoom : MonoBehaviour
{
    GameObject key;
    [Tooltip("현재 진행하고 있는 미션 번호")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("미션 진행 상황이 몇 %일 때 이벤트를 active할 것인지 지정")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("작업 완료 시 미션 진행 상황 설정")]
    [SerializeField] float missionProgress;

    void Start()
    {
        key = transform.Find("1-2 열쇠").gameObject;
    }

    private void Update()
    {
        if (GameObject.FindObjectOfType<GameManager>().Mission1Progress == activeByMissionProgress) // 몬스터 점프 스케어 이후
            key.SetActive(true);
        else
            key.SetActive(false);
    }
}
