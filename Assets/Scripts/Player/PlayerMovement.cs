using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement player;
    public static string CurrentMapName; //���� ���� �����ΰ�.
    Rigidbody2D rigid;
    public Animator animator;
    public float speed;
    public float normalSpeed, crawlSpeed, runSpeed;
    Vector2 movement;
    public Vector2 direction; // �÷��̾ ���� ���ϰ� �ִ� ����

    public int numOfAmulets; // �÷��̾ ������ �ִ� ���� ����

    //ī�޶� �����
    public GameObject MainCamera;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } // �ߺ��� Player ������Ʈ�� ���� ��� ������Ʈ �ı�
        
        rigid = GetComponent<Rigidbody2D>();
        numOfAmulets = 3;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (numOfAmulets > 0)
            {
                animator.SetTrigger("UseAmulet");
                if (Vector3.Distance(transform.position, GameObject.Find("Monster").transform.position) < 50f)
                    GameObject.Find("Monster").GetComponent<MonsterStatus>().attacked = true;
                //numOfAmulets--;
            }
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