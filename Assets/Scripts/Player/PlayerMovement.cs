using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public float speed;
    public float normalSpeed, crawlSpeed, runSpeed; 
    Vector2 movement;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;

        // 이동에 따른 애니메이션
        animator.SetInteger("WalkHorizontally", (int)movement.x);
        animator.SetInteger("WalkVertically", (int)movement.y);

        ChangeMoveSpeed();
    }

    void FixedUpdate()
    {
        // 이동
        rigid.MovePosition(rigid.position + movement * speed * Time.deltaTime);
    }

    // 키 입력에 따른 이동 속도 변경
    void ChangeMoveSpeed()
    {
        // 기어가기
        if (Input.GetKey(KeyCode.C))
        {
            speed = crawlSpeed;
            animator.SetFloat("MoveSpeed", 0.5f);
        }
        // 뛰기
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            animator.SetFloat("MoveSpeed", 2f);
        }
        // 일반 속도
        else
        {
            speed = normalSpeed;
            animator.SetFloat("MoveSpeed", 1f);
        }
    }
}
