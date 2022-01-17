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

        // �̵��� ���� �ִϸ��̼�
        animator.SetInteger("WalkHorizontally", (int)movement.x);
        animator.SetInteger("WalkVertically", (int)movement.y);

        ChangeMoveSpeed();
    }

    void FixedUpdate()
    {
        // �̵�
        rigid.MovePosition(rigid.position + movement * speed * Time.deltaTime);
    }

    // Ű �Է¿� ���� �̵� �ӵ� ����
    void ChangeMoveSpeed()
    {
        // ����
        if (Input.GetKey(KeyCode.C))
        {
            speed = crawlSpeed;
            animator.SetFloat("MoveSpeed", 0.5f);
        }
        // �ٱ�
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            animator.SetFloat("MoveSpeed", 2f);
        }
        // �Ϲ� �ӵ�
        else
        {
            speed = normalSpeed;
            animator.SetFloat("MoveSpeed", 1f);
        }
    }
}
