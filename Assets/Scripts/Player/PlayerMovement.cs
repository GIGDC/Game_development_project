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
        // �̵�
        rigid.MovePosition(rigid.position + movement * speed * Time.deltaTime);
    }

    // Ű �Է¿� ���� �̵� �ӵ� ����
    void ChangeMoveSpeed()
    {
        // ����
        if (Input.GetKey(KeyCode.C))
            speed = crawlSpeed;
        // �ٱ�
        else if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        // �Ϲ� �ӵ�
        else
            speed = normalSpeed;

        animator.SetFloat("MoveSpeed", movement.sqrMagnitude * speed);
        animator.speed = speed / 4; // �̵� �ӵ��� 4���� 1��ŭ�� ������� �ִϸ��̼� ���
    }
}
