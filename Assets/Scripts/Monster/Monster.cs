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

    Transform targetTransform = null;
    Vector3 endPosition;
    float currenAngle = 0;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        currentSpeed = wanderSpeed;
        StartCoroutine(WanderRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rigid.position, endPosition, Color.red);
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

        Vector3 direction = Vector3FromAngle(currenAngle);
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
        else if (-0.9 < direction.x && direction.x <0.9 && direction.y > 0)
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
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
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

                rigid.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("isWalking", false);
    }
}
