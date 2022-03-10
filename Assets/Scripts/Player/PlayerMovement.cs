using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement player;
    public string CurrentMapName; //���� ���� �����ΰ�.
    Rigidbody2D rigid;
    public Animator animator;
    public float speed;
    public float normalSpeed, crawlSpeed, runSpeed;
    Vector2 movement;
    public Vector2 direction; // �÷��̾ ���� ���ϰ� �ִ� ����

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        if (player == null)
        {
            DontDestroyOnLoad(this.gameObject); // memory leak
            rigid = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.sqrMagnitude > 0)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");
        }

        ChangeMoveSpeed();
        if (animator.GetFloat("MoveSpeed") > 0)
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

    public Vector2 GetDirectionNormalized()
    {
        return direction.normalized;
    }
}