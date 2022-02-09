using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Monster : MonoBehaviour
{
    public float pursuitSpeed;  // monster�� �÷��̾ �����ϴ� �ӵ�
    public float wanderSpeed;   // ���� monster�� �ӵ�
    public float currentSpeed;  // ���� �ӵ�
    public float directionChangeInterval;
    Transform target; //player

    [Header("���� �Ÿ�")]
    [SerializeField]
    [Range(0f, 3f)]
    float contactDistance = 3f; //����Ƽ���� �����ϰ� ���������ϵ�����

    Coroutine moveCoroutine;
    Coroutine wanderCoroutine;
    Rigidbody2D rigid;
    Animator animator;
    bool trackControl = false; //������ ���� �������� player�� ��ġ�Ҷ� true/ ��ġ���������� false
    static public Vector3 endPosition;
    static public Vector3 direction;
    static public bool Stop = false;
    static public bool Bugfix = false;
    // Start is called before the first frame update

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        init(); //���� �ʱ�ȭ �� �迭�ʱ�ȭ
        wanderCoroutine = StartCoroutine(WanderRoutine());
    }

    void init()
    {
        endPosition = transform.position;
        currentSpeed = wanderSpeed;
        pursuitSpeed = wanderSpeed * 2f;
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
    private void OnTriggerEnter2D(Collider2D collision) //�߰����� zone������ ���˸鿡 ������ true
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            trackControl = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //�߰����� zone ������ ���˸鿡�� �������� false
    {
        trackControl = false;

    }

    public IEnumerator WanderRoutine()  // �÷��̾ �������� �ʰ� ��ȸ�ϴ� monster
    {
        while (true)
        {

            if (TransferMap.CheckMonster)
            {
                Destroy(gameObject);
            }
                ChooseNewEndPoint();  // ���� ������ ����

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

            // ���ݹް� ���� �� ������ �Ͻ�����
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