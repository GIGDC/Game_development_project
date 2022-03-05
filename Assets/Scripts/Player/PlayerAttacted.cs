using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacted : MonoBehaviour
{
    public Slider slider;
    public int hp;
    public int maxHP;

    public Sprite attack_img;

    public bool zeroHP = false;
    public Animator AttackAnimation;

    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        maxHP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)hp / maxHP;

        if (hp == 0)
        {
            zeroHP = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            hp = hp - 10;

            print(hp);
            StartCoroutine(ChangeAttack());
        }
    }

    private IEnumerator ChangeAttack()
    {
        AttackAnimation.SetInteger("isAttack", 1);   // 공격받은 모션
        yield return new WaitForSeconds(0.5f);

        if (hp < 100 & hp >= 70)  // hp 70~99 모션
        {
            AttackAnimation.SetInteger("isAttack", 2);
        }
        else if (hp >= 50)  // hp 50~69 모션
        {
            AttackAnimation.SetInteger("isAttack", 3);
        }
        else if (hp >= 20)  // hp 20~49 모션
        {
            AttackAnimation.SetInteger("isAttack", 4);
        }
        else if (hp >= 1)  // hp 1~19 모션
        {
            AttackAnimation.SetInteger("isAttack", 5);
        }
    }
}
