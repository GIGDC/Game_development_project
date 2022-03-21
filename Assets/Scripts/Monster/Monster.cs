using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Monster : MonoBehaviour
{
    public float pursuitSpeed;  // monster가 플레이어를 추적하는 속도
    public float wanderSpeed;   // 평상시 monster의 속도
    public float currentSpeed;  // 현재 속도
    public float directionChangeInterval;
    Transform target; //player

    [Header("근접 거리")]
    [SerializeField]
    [Range(0f, 3f)]
    float contactDistance = 3f; //유니티에서 간편하게 조절가능하도록함

    Coroutine moveCoroutine;
    public Coroutine wanderCoroutine;
    Rigidbody2D rigid;
    Animator animator;
    bool trackControl = false; //몬스터의 추적 공간내에 player가 위치할때 true/ 위치하지않으면 false
    static public Vector3 endPosition;
    static public Vector3 direction;
    
    static public bool Stop = false;
    static public bool Bugfix = false;


    private Timer_60 clock;
    // Start is called before the first frame update

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        clock = GameObject.FindObjectOfType<Timer_60>(); // Timer_60에 대한 clock을 찾음

        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        init(); //변수 초기화 및 배열초기화
        wanderCoroutine = StartCoroutine(WanderRoutine());
    }

    void init()
    {
        endPosition = transform.position;
        currentSpeed = wanderSpeed;
        pursuitSpeed = wanderSpeed * 2f;
    }
    public void Hide()
    {
        if(wanderCoroutine!=null)
            StopCoroutine(wanderCoroutine);
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        gameObject.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Stop = true;
            trackControl = false;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Bugfix = true;
    }
    private void OnTriggerEnter2D(Collider2D collision) //추격자의 zone영역의 접촉면에 닿으면 true
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            trackControl = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //추격자의 zone 영역의 접촉면에서 떨어지면 false
    {
        trackControl = false;

    }

    public IEnumerator WanderRoutine()  // 플레이어를 추적하지 않고 배회하는 monster
    {
        while (true)
        {
            if (Transfer.CheckMonster)
            {

                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);

                Hide();
                //Destroy(gameObject); -> 게임 오브젝트 삭제 
            }
                ChooseNewEndPoint();  // 향할 목적지 선택

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rigid, currentSpeed));

            yield return new WaitForSeconds(directionChangeInterval);

        }
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }


    void ChooseNewEndPoint()
    {

        if (direction.x > 0 && -0.9 < direction.y && direction.y < 0.9)
        {
            animator.SetFloat("DirX", 1);
            animator.SetFloat("DirY", 0);
        }
        else if (direction.x < 0 && -0.9 < direction.y && direction.y < 0.9)
        {
            animator.SetFloat("DirX", -1);
            animator.SetFloat("DirY", 0);
        }
        else if (-0.9 < direction.x && direction.x < 0.9 && direction.y > 0)
        {
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirY", -1);
        }
        else
        {
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirY", 1);
        }

        endPosition += direction;

    }

    public IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            if (target != null)
            {
                if (Vector2.Distance(transform.position, target.position) > contactDistance && trackControl)
                {
                    Vector3 v = target.position - transform.position;
                    direction = Vector3FromAngle(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
                    speed = pursuitSpeed;
                    endPosition = target.position;
                }
            }

            if (Bugfix)
            {
                if (endPosition.y > -12 && endPosition.y < 5)
                {
                    Monster.direction = MonsterCollision.Vector3FromAngle(90, 90);
                }
                else
                {
                    Bugfix = false;
                    Stop = false;
                }
            }

            // 공격받고 있을 때 움직임 일시정지
            if (GetComponent<MonsterStatus>().Attacked || Stop)
            {
                animator.SetBool("isWalking", false);
            }

            else if (rigidBodyToMove != null)
            {
                animator.SetBool("isWalking", true);
                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, endPosition, speed * Time.deltaTime);
                speed = wanderSpeed;
                rigid.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("isWalking", false);
    }
}