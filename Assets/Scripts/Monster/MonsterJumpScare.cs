using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterJumpScare : MonoBehaviour
{
    public GameObject monster;

    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) // 점프 스케어 애니메이션 종료
        {
            monster.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
