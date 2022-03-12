using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    static Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    static public IEnumerator ChangeAttack()
    {

        animator.SetInteger("isAttack", 1);   // 공격받은 모션
        yield return new WaitForSeconds(0.5f);

        if (PlayerAttacted .hp < 100 & PlayerAttacted.hp >= 70)  // hp 70~99 모션
        {
            animator.SetInteger("isAttack", 2);
        }
        else if (PlayerAttacted.hp >= 50)  // hp 50~69 모션
        {
            animator.SetInteger("isAttack", 3);
        }
        else if (PlayerAttacted.hp >= 20)  // hp 20~49 모션
        {
            animator.SetInteger("isAttack", 4);
        }
        else if (PlayerAttacted.hp >= 1)  // hp 1~19 모션
        {
            animator.SetInteger("isAttack", 5);
        }
    }
}
