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

    float time = 0;
    bool door = false;

    int count = -1;
    Coroutine moveCoroutine;
    Coroutine startCoroutine;
    Rigidbody2D rigid;
    Animator animator;
    Transform target=null; //player

    Vector3 endPosition;
    
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
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
  
    private void OnTriggerEnter2D(Collider2D collision) //�߰����� zone������ ���˸鿡 ������ true
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision) //�߰����� zone ������ ���˸鿡�� �������� false
    {
        if (count < 0)
        {
            if (collision.gameObject.CompareTag("Door"))
            {
                endPosition = transform.position;
                direction = Monster.Vector3FromAngle(90, 90);
                door = true;
                count++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            endPosition = transform.position;
            Stop = true;
    }

    void DisappearMonster()
    {
        if (time < 0.5f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f-time / 1);
        }
        else
        {
            time = 0;
            this.gameObject.SetActive(false);
            door = false;
            endPosition = transform.position;
        }
        time += Time.deltaTime;
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
            if (transform.position.x>-14f)
            {

                Stop = false;
            }
        }
        if (Up_Collision)
        {

            if (transform.position.y <9.5f)
            {
                Stop = false;
            }
        }
        if (Right_Collision)
            Stop = false;

        if (Down_Collision)
        {
            if(endPosition.y>-16f) //�̿� ���� ������ �ӵ��� ���� �޶����� ����. �ӵ��� ���� ������ ������ �ޱ⶧�� �ش���� : endPosition.y�� �ش�//
            {
                Stop = false;
            }
        }

    }

    void ChooseNewEndPoint()
    {
        if(!door)
            Collision_false();
        else
        {
            DisappearMonster();
        }

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

        
        if (Stop&&!door)
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