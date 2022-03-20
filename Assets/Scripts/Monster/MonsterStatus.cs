using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    PlayerMovement player;
    public int stamina;
    public float stunnedTime;
    public bool attacked;
    private bool attacking;
    private float toggleAttackedTimer; // attacked�� true���� false�� �ٲ� ������ �ð��� �����Ǵ� Ÿ�̸� 

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player=GameObject.FindObjectOfType<PlayerMovement>();
        attacked = false;
        attacking = false;
        toggleAttackedTimer = 0;
    }

    void Update()
    {
        // ���� �ð�(stunnedTime)���� ���Ͱ� ���� �޴� ������ ����
        if(attacked)
        {
            if (toggleAttackedTimer < stunnedTime)
                toggleAttackedTimer += Time.deltaTime;
                
            else
            {
                toggleAttackedTimer = 0;
                attacked = false;
                player.isAttacking = false;
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
