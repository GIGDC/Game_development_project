using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterJumpScare : MonoBehaviour
{
    public GameObject monster;
    [Tooltip("���� �����ϰ� �ִ� �̼� ��ȣ")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("�̼� ���� ��Ȳ�� �� %�� �� �̺�Ʈ�� active�� ������ ����")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("�۾� �Ϸ� �� �̼� ���� ��Ȳ ����")]
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
        // ����� �̼� 1��
        if (GameObject.FindObjectOfType<GameManager>().Mission1Progress == activeByMissionProgress)
        {
            this.gameObject.SetActive(true);
        }
        else
            this.gameObject.SetActive(false);

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) // ���� ���ɾ� �ִϸ��̼� ����
        {
            monster.SetActive(true);
            this.gameObject.SetActive(false);
            GameObject.FindObjectOfType<GameManager>().Mission1Progress = missionProgress;
        }
    }
}
