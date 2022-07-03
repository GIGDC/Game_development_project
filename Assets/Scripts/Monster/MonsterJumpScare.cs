using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterJumpScare : MonoBehaviour
{
    public GameObject monster;

    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) // ���� ���ɾ� �ִϸ��̼� ����
        {
            monster.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
