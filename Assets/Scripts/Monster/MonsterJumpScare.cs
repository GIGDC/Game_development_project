using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterJumpScare : MonoBehaviour
{
    public GameObject monster;
    [Tooltip("현재 진행하고 있는 미션 번호")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("미션 진행 상황이 몇 %일 때 이벤트를 active할 것인지 지정")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("작업 완료 시 미션 진행 상황 설정")]
    [SerializeField] float missionProgress;

    public AudioSource audio;
    public AudioClip bgm;
    private void Start()
    {
        audio.clip = bgm;
        audio.Play();
    }
    void Update()
    {
        // 현재는 미션 1만
        if (GameObject.FindObjectOfType<GameManager>().Mission1Progress == activeByMissionProgress)
        {
            this.gameObject.SetActive(true);
        }
        else
            this.gameObject.SetActive(false);

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) // 점프 스케어 애니메이션 종료
        {
            monster.SetActive(true);
            this.gameObject.SetActive(false);
            GameObject.FindObjectOfType<GameManager>().Mission1Progress = missionProgress;
        }
    }
}
