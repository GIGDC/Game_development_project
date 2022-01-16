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

    Coroutine moveCoroutine;
    Rigidbody2D rigid;
    Animator animator;

    
    Transform target; //player

    Vector3 endPosition;
    Vector3 direction;
    float currenAngle = 0;
    bool trackControl = false; //������ ���� �������� player�� ��ġ�Ҷ� true/ ��ġ���������� false

    [Header("���� �Ÿ�")]
    [SerializeField] [Range(0f, 3f)] float contactDistance = 1f; //����Ƽ���� �����ϰ� ���������ϵ�����

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       
        currentSpeed = wanderSpeed;
        pursuitSpeed = wanderSpeed * 2f;
        StartCoroutine(WanderRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rigid.position, endPosition, Color.red);

    }
    private void OnTriggerEnter2D(Collider2D collision) //�߰����� zone������ ���˸鿡 ������ true
    {
        trackControl = true;
    }
    private void OnTriggerExit2D(Collider2D collision) //�߰����� zone ������ ���˸鿡�� �������� false
    {
        trackControl = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            print("���� �����");
            StopCoroutine(moveCoroutine);
            endPosition += direction * (-1);
            moveCoroutine = StartCoroutine(Move(rigid, currentSpeed));
        }
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

    void ChooseNewEndPoint()
    {
        currenAngle += Random.Range(0, 360);
        currenAngle = Mathf.Repeat(currenAngle, 360);
        direction = Vector3FromAngle(currenAngle);

        if (trackControl)
        {
            Vector3 v = target.position - transform.position;
            direction = Vector3FromAngle(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
        }

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

    Vector3 Vector3FromAngle(float inputAngleDegrees)
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
                    endPosition = Vector3.MoveTowards(rigid.position, target.position, pursuitSpeed * Time.deltaTime);
                    speed = pursuitSpeed;
                }
            }

            if (rigidBodyToMove != null)
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
