using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public int stamina;
    public float stunnedTime;
    public bool attacked;
    private bool attacking;
    private float toggleAttackedTimer; // attacked가 true에서 false로 바뀔 때까지 시간이 누적되는 타이머 

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        attacked = false;
        attacking = false;
        toggleAttackedTimer = 0;
    }

    void Update()
    {
        // 일정 시간(stunnedTime)동안 몬스터가 공격 받는 것으로 간주
        if(attacked)
        {
            if (toggleAttackedTimer < stunnedTime)
                toggleAttackedTimer += Time.deltaTime;
            else
            {
                toggleAttackedTimer = 0;
                attacked = false;
            }
        }
    }

    public bool Attacked
    {
        get { return attacked; }
        set { 
            attacked = value;
            stamina--;
        }
    }

    public bool Attacking
    {
        get { return attacking; }
        set { attacking = value; }
    }
}
