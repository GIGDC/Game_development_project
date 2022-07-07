using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInSpecialEdRoom : MonoBehaviour
{
    GameObject key;
    [Tooltip("���� �����ϰ� �ִ� �̼� ��ȣ")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("�̼� ���� ��Ȳ�� �� %�� �� �̺�Ʈ�� active�� ������ ����")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("�۾� �Ϸ� �� �̼� ���� ��Ȳ ����")]
    [SerializeField] float missionProgress;

    void Start()
    {
        key = transform.Find("1-2 ����").gameObject;
    }

    private void Update()
    {
        if (GameObject.FindObjectOfType<GameManager>().Mission1Progress == activeByMissionProgress) // ���� ���� ���ɾ� ����
            key.SetActive(true);
        else
            key.SetActive(false);
    }
}
