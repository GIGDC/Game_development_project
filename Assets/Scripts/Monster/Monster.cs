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
    public bool trackControl; //������ ���� �������� player�� ��ġ�Ҷ� true/ ��ġ���������� false

 /*ai �ൿ ������ ���� bool �Լ� */
    static public bool Down_Collision = false;
    static public bool Up_Collision = false;
    static public bool Left_Collision = false;
    static public bool Right_Collision = false;
    static public Vector3 direction; //��ǥ ����
    static public bool Stop = false;
    static public bool check_pattern = false;
 /*********************************/
    [Header("���� �Ÿ�")]
    [SerializeField] [Range(0f, 3f)] float contactDistance = 1f; //����Ƽ���� �����ϰ� ���������ϵ�����

    Coroutine moveCoroutine;
    Rigidbody2D rigid;
    Animator animator;
    Transform target=null; //player

    Vector3 endPosition;
    CircleCollider2D CircleCollider2D;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        CircleCollider2D = GetComponent<CircleCollider2D>();
        init(); //���� �ʱ�ȭ �� �迭�ʱ�ȭ
        StartCoroutine(WanderRoutine());
    }

    void init()
    {
        endPosition = transform.position;
        currentSpeed = wanderSpeed;
        pursuitSpeed = wanderSpeed * 2f;
        direction = Vector3FromAngle(180, 0);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rigid.position, endPosition, Color.red);

    }
    void OnDrawGizmos()
    {
        if (CircleCollider2D != null)
        {
            Gizmos.DrawWireSphere(transform.position, CircleCollider2D.radius);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //�߰����� zone������ ���˸鿡 ������ true
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

    private void OnTriggerExit2D(Collider2D collision) //�߰����� zone ������ ���˸鿡�� �������� false
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        endPosition = transform.position;
        Stop = true;
    }

    public IEnumerator WanderRoutine()  // �÷��̾ �������� �ʰ� ��ȸ�ϴ� monster
    {
        while (true)
        {
            ChooseNewEndPoint();  // ���� ������ ����
            
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rigid, currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void Collision_false()
    {
        if (!Stop&&!check_pattern)
        {
            if (Left_Collision)
            {
                direction = Vector3FromAngle(90, 90);
                Left_Collision = false;
            }
            if (Up_Collision)
            {
                direction = Vector3FromAngle(0, 180);
                Up_Collision = false;
            }
            if (Down_Collision)
            {
                direction = Vector3FromAngle(90, 90);
                Down_Collision = false;
            }
            if (Right_Collision)
            {
                direction = Vector3FromAngle(180, 0);
                Right_Collision = false;
                check_pattern = true;
            }
        }
        else if(!Stop&&check_pattern)
        {
            if (Left_Collision)
            {
                direction = Vector3FromAngle(90, 270);
                Left_Collision = false;
            }
            if (Down_Collision)
            {
                direction = Vector3FromAngle(0, 180);
                Down_Collision = false;
            }
            if (Right_Collision)
            {
                direction = Vector3FromAngle(180, 0);
                Right_Collision = false;
                check_pattern = false;
            }
            
        }
    }

    void GoToTheCenter()
    {
        
        if (Left_Collision)
        {
            if (endPosition.x > 0)
            {
                Stop = false;
            }
        }
        if (Up_Collision)
        {

            if (endPosition.y <-5)
            {
                Stop = false;
            }
        }
        if (Right_Collision)
            Stop = false;

        if (Down_Collision)
        {
            if(endPosition.y>5) //�̿� ���� ������ �ӵ��� ���� �޶����� ����. �ӵ��� ���� ������ ������ �ޱ⶧�� �ش���� : endPosition.y�� �ش�//
            {
                Stop = false;
            }
        }

    }

    void ChooseNewEndPoint()
    {
        
        Collision_false();

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

        if (Stop)
        {
            GoToTheCenter();
        }

        endPosition += direction;

    }

    static public Vector3 Vector3FromAngle(float x, float y) //AI �̵�
    {
        float inputAngleRadians1 = x * Mathf.Deg2Rad;
        float inputAngleRadians2 = y * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians1), Mathf.Sin(inputAngleRadians2), 0);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees) // Player ������ �ʿ�
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
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

            // ���ݹް� ���� �� ������ �Ͻ�����
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