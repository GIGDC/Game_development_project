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

        ChangeMoveSpeed();
        if(animator.GetFloat("MoveSpeed") > 0)
        {
            animator.SetFloat("MoveHorizontally", movement.x);
            animator.SetFloat("MoveVertically", movement.y);
        }
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
            speed = crawlSpeed;
        // 뛰기
        else if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        // 일반 속도
        else
            speed = normalSpeed;

        animator.SetFloat("MoveSpeed", movement.sqrMagnitude * speed);
        animator.speed = speed / 4; // 이동 속도의 4분의 1만큼의 빠르기로 애니메이션 재생
    }
}
