using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    /* ���ο� ai �ൿ ������ ���� Raycast ����*/
    RaycastHit2D hitRight;
    RaycastHit2D hitLeft;
    RaycastHit2D hitUp; //back�� �����ʴ´�. ������, ����, ���� �� ������ ��� �ڷ� ����.
    /*****************************************/
    float MaxDistance = 7f;
    // Update is called once per frame
    float directionChangeInterval = 0;
    Vector2 right;
    Vector2 left;
    Vector2 front;
    bool Xcol = false; //x��ǥ �ε���
    bool Irreversible = false; //ai�� ���� �ö�� �� or �Ʒ��� ������ �� �� �쿡 �ε��������� �ڷ� ���� ���� �ϱ� ���� ����
    static public Coroutine monster;

    void Start()
    {
        Monster.direction = Vector3FromAngle(180, 0);
    }

    void Update()
    {
        if (!Monster.Stop)
        {
            MonsterDirection();
            MonsterRaycast();
        }
    }
    void MonsterDirection()
    {
        if (Monster.direction.x == -1)
        {
            right = transform.up;
            left = -transform.up;
            front = -transform.right;
        }
        else if (Monster.direction.x == 1)
        {

            right = -transform.up;
            left = transform.up;
            front = transform.right;
        }

        if (Monster.direction.y == -1)
        {
            right = -transform.right;
            left = transform.right;
            front = -transform.up;
        }
        else if (Monster.direction.y == 1)
        {
            right = transform.right;
            left = -transform.right;
            front = transform.up;
        }
    }

    static public Vector3 Vector3FromAngle(float x, float y) //Ai �̵�
    {
        float inputAngleRadians1 = x * Mathf.Deg2Rad;
        float inputAngleRadians2 = y * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians1), Mathf.Sin(inputAngleRadians2), 0);
    }

    void MonsterRaycast()
    {

        //���� - ĳ���� ���� ������
        hitRight = Physics2D.Raycast(transform.position, right, MaxDistance, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, right * MaxDistance, Color.red);

        //��� - ĳ���� ���� ����
        hitLeft = Physics2D.Raycast(transform.position, left, MaxDistance, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, left * MaxDistance, Color.blue);

        //���ο� -ĳ���� ���� ��
        hitUp = Physics2D.Raycast(transform.position, front, 5f, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, front * 5f, Color.yellow);

        Monster.endPosition = transform.position;

        bool op; //0�̸� false, 1�� true ������ ���� ����.
        if (hitUp) //���̸������ 
        {
            Debug.Log("�ո���");
            if (hitLeft && hitRight) //�� �� ������� 
            {
                if (!Xcol) //���� -> ���������� 
                {
                    Xcol = true;
                    Irreversible = false;
                    Monster.direction = Vector3FromAngle(0, 180);
                    Debug.Log("��->��");
                }
                else //������ -> �������� ����
                {
                    Xcol = false;
                    Irreversible = false;
                    Monster.direction = Vector3FromAngle(180, 0);
                    Debug.Log("��->��");
                }
            }
            else if (!hitLeft && !hitRight) //�ո� �������
            {
                op = ((Random.Range(0, 2) % 2) != 0);
                Debug.Log("��(true) / ��(false) " + op);
                if (op)
                {
                    Irreversible = true;
                    Debug.Log("���� ����, ��ȸ��");
                    Monster.direction = Vector3FromAngle(180, 0);
                }
                else
                {
                    Irreversible = true;
                    Debug.Log("���� ����, ��ȸ��");
                    Monster.direction = Vector3FromAngle(0, 180);
                }
            }
        }

        else //���� ������ ���� ���!
        {
            if (!Irreversible)
            {
                if (hitRight && !hitLeft) //��-������ ������ ���� ��� 
                {
                    if (Monster.endPosition.x > -9f && Monster.endPosition.y < -13f)
                    {
                        op = ((Random.Range(0, 2) % 2) != 0);
                        if (op) //�Ʒ���
                        {
                            Debug.Log("��-���� ����������, ����");
                            Monster.direction = Vector3FromAngle(0, 180);
                        }
                        else
                        {
                            Debug.Log("��-���� ����������, ��ȸ��");
                            Monster.direction = Vector3FromAngle(90, 90);
                        }
                    }
                    if (Monster.endPosition.x < -9f && Monster.endPosition.y > 5f)
                    {
                        op = ((Random.Range(0, 2) % 2) != 0);
                        if (op) //��-������ ������ �������, ����) �ΰ��
                        {
                            Monster.direction = Vector3FromAngle(180, 0);
                            Debug.Log("��-���� ����������, ����");
                        }
                        else//������
                        {
                            Monster.direction = Vector3FromAngle(90, 270);
                            Debug.Log("��-���� ����������, ��ȸ��");
                        }
                    }
                }
            }
            else if (hitLeft && !hitRight) //��-�������� �ȸ������
            {
                if (!Irreversible)
                {
                    if (Monster.endPosition.x < -9f && Monster.endPosition.y < -13f)
                    {
                        op = ((Random.Range(0, 2) % 2) != 0);
                        if (op)
                        {
                            Debug.Log("��-�������� ����������, ��ȸ��");
                            Monster.direction = Vector3FromAngle(90, 90);
                        }
                        else
                        {
                            Debug.Log("��-�������� ����������, ����");
                            Monster.direction = Vector3FromAngle(180, 0);
                        }
                    }

                    if (Monster.endPosition.x > -9f && Monster.endPosition.y > 5f)
                    {
                        op = ((Random.Range(0, 2) % 2) != 0);
                        if (op)
                        {
                            Debug.Log("��-�������� ����������(����), ����");
                            Monster.direction = Vector3FromAngle(0, 180);
                        }
                        else
                        {
                            Debug.Log("��-�������� ����������(����), ��ȸ��");
                            Monster.direction = Vector3FromAngle(90, 270);
                        }
                    }
                }
            }
        }
        // Debug.Log("Irreversible : " + Irreversible);
    }
}