using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement player;
    public static string CurrentMapName; //현재 맵은 무엇인가.
    Rigidbody2D rigid;
    public Animator animator;
    public float speed;
    public float normalSpeed, crawlSpeed, runSpeed;
    Vector2 movement;
    public Vector2 direction; // 플레이어가 현재 향하고 있는 방향

    public int numOfAmulets; // 플레이어가 가지고 있는 부적 개수

    //카메라 만들기
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
        } // 중복된 Player 오브젝트가 있을 경우 오브젝트 파괴
        
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

    public Vector2 GetDirectionNormalized()
    {
        return direction.normalized;
    }
}