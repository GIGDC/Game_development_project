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
    public bool trackControl; //몬스터의 추적 공간내에 player가 위치할때 true/ 위치하지않으면 false

 [Header("근접 거리")]
    [SerializeField] [Range(0f, 3f)] float contactDistance = 1f; //유니티에서 간편하게 조절가능하도록함

    Coroutine moveCoroutine;
    Rigidbody2D rigid;
    Animator animator;
    Transform target=null; //player

    static public Vector3 endPosition;
    static public Vector3 direction;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        init(); //변수 초기화 및 배열초기화
        StartCoroutine(WanderRoutine());
    }

    void init()
    {
        endPosition = transform.position;
        currentSpeed = wanderSpeed;
        pursuitSpeed = wanderSpeed * 2f;
    }

    private void OnTriggerEnter2D(Collider2D collision) //추격자의 zone영역의 접촉면에 닿으면 true
    {

        if (collision.gameObject.CompareTag("Player")&&trackControl)
        {
          //  currentSpeed = pursuitSpeed;
          //  target = collision.gameObject.transform;
          //  Vector3 v = target.position - transform.position;
          //  direction = Vector3FromAngle(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);

       //     Debug.Log(direction);
         //   if (moveCoroutine != null)
          //  {
          //      StopCoroutine(moveCoroutine);
           // }
           // moveCoroutine = StartCoroutine(Move(rigid, currentSpeed));
        }

    }

    private void OnTriggerExit2D(Collider2D collision) //추격자의 zone 영역의 접촉면에서 떨어지면 false
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            target = null;
        }
    }

    public IEnumerator WanderRoutine()  // 플레이어를 추적하지 않고 배회하는 monster
    {
        while (true)
        {
            ChooseNewEndPoint();  // 향할 목적지 선택
            
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rigid, currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    
    void ChooseNewEndPoint()
    {
        
        
        if (direction.x > 0 && -0.9 < direction.y && direction.y < 0.9)
        {

            animator.SetFloat("DirX", 1);
            animator.SetFloat("DirY", 0);
        }
        else if (direction.x <= 0 && -0.9 < direction.y && direction.y < 0.9)
        {

            animator.SetFloat("DirX", -1);
            animator.SetFloat("DirY", 0);
        }
        else if (-0.9 < direction.x && direction.x < 0.9 && direction.y >= 0)
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

    static public Vector3 Vector3FromAngle(float x, float y) //AI 이동
    {
        float inputAngleRadians1 = x * Mathf.Deg2Rad;
        float inputAngleRadians2 = y * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians1), Mathf.Sin(inputAngleRadians2), 0);
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
                    endPosition = target.position;
                }
            }

            // 공격받고 있을 때 움직임 일시정지
            if (GetComponent<MonsterStatus>().Attacked)
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