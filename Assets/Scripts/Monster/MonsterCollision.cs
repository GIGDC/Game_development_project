using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    /* 새로운 ai 행동 패턴을 위한 Raycast 변수*/
    RaycastHit2D hitRight;
    RaycastHit2D hitLeft;
    RaycastHit2D hitUp; //back은 쏘지않는다. 오른쪽, 왼쪽, 앞이 다 감지될 경우 뒤로 돈다.
    /*****************************************/
    float MaxDistance = 7f;
    // Update is called once per frame
    float directionChangeInterval = 0;
    Vector2 right;
    Vector2 left;
    Vector2 front;
    bool Xcol = false; //x좌표 부딪힘
    bool Irreversible = false; //ai는 위로 올라온 후 or 아래로 내려간 후 좌 우에 부딪힐때까지 뒤로 갈수 없게 하기 위한 변수
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

    static public Vector3 Vector3FromAngle(float x, float y) //Ai 이동
    {
        float inputAngleRadians1 = x * Mathf.Deg2Rad;
        float inputAngleRadians2 = y * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians1), Mathf.Sin(inputAngleRadians2), 0);
    }

    void MonsterRaycast()
    {

        //레드 - 캐릭터 기준 오른쪽
        hitRight = Physics2D.Raycast(transform.position, right, MaxDistance, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, right * MaxDistance, Color.red);

        //블루 - 캐릭터 기준 왼쪽
        hitLeft = Physics2D.Raycast(transform.position, left, MaxDistance, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, left * MaxDistance, Color.blue);

        //옐로우 -캐릭터 기준 위
        hitUp = Physics2D.Raycast(transform.position, front, 5f, LayerMask.GetMask("Platform"));
        Debug.DrawRay(transform.position, front * 5f, Color.yellow);

        Monster.endPosition = transform.position;

        bool op; //0이면 false, 1면 true 에따라 길을 고른다.
        if (hitUp) //앞이막힌경우 
        {
            Debug.Log("앞막힘");
            if (hitLeft && hitRight) //좌 우 막힌경우 
            {
                if (!Xcol) //왼쪽 -> 오른쪽으로 
                {
                    Xcol = true;
                    Irreversible = false;
                    Monster.direction = Vector3FromAngle(0, 180);
                    Debug.Log("좌->우");
                }
                else //오른쪽 -> 왼쪽으로 변경
                {
                    Xcol = false;
                    Irreversible = false;
                    Monster.direction = Vector3FromAngle(180, 0);
                    Debug.Log("우->좌");
                }
            }
            else if (!hitLeft && !hitRight) //앞만 막힌경우
            {
                op = ((Random.Range(0, 2) % 2) != 0);
                Debug.Log("좌(true) / 우(false) " + op);
                if (op)
                {
                    Irreversible = true;
                    Debug.Log("앞이 막힘, 좌회전");
                    Monster.direction = Vector3FromAngle(180, 0);
                }
                else
                {
                    Irreversible = true;
                    Debug.Log("앞이 막힘, 우회전");
                    Monster.direction = Vector3FromAngle(0, 180);
                }
            }
        }

        else //앞이 막히지 않은 경우!
        {
            if (!Irreversible)
            {
                if (hitRight && !hitLeft) //앞-왼쪽이 막히지 않은 경우 
                {
                    if (Monster.endPosition.x > -9f && Monster.endPosition.y < -13f)
                    {
                        op = ((Random.Range(0, 2) % 2) != 0);
                        if (op) //아랫층
                        {
                            Debug.Log("앞-왼쪽 막히지않음, 직진");
                            Monster.direction = Vector3FromAngle(0, 180);
                        }
                        else
                        {
                            Debug.Log("앞-왼쪽 막히지않음, 좌회전");
                            Monster.direction = Vector3FromAngle(90, 90);
                        }
                    }
                    if (Monster.endPosition.x < -9f && Monster.endPosition.y > 5f)
                    {
                        op = ((Random.Range(0, 2) % 2) != 0);
                        if (op) //앞-왼쪽이 막히지 않은경우, 윗층) 인경우
                        {
                            Monster.direction = Vector3FromAngle(180, 0);
                            Debug.Log("앞-왼쪽 막히지않음, 직진");
                        }
                        else//밑으로
                        {
                            Monster.direction = Vector3FromAngle(90, 270);
                            Debug.Log("앞-왼쪽 막히지않음, 좌회전");
                        }
                    }
                }
            }
            else if (hitLeft && !hitRight) //앞-오른쪽이 안막힌경우
            {
                if (!Irreversible)
                {
                    if (Monster.endPosition.x < -9f && Monster.endPosition.y < -13f)
                    {
                        op = ((Random.Range(0, 2) % 2) != 0);
                        if (op)
                        {
                            Debug.Log("앞-오른쪽이 막히지않음, 우회전");
                            Monster.direction = Vector3FromAngle(90, 90);
                        }
                        else
                        {
                            Debug.Log("앞-오른쪽이 막히지않음, 직진");
                            Monster.direction = Vector3FromAngle(180, 0);
                        }
                    }

                    if (Monster.endPosition.x > -9f && Monster.endPosition.y > 5f)
                    {
                        op = ((Random.Range(0, 2) % 2) != 0);
                        if (op)
                        {
                            Debug.Log("앞-오른쪽이 막히지않음(윗쪽), 직진");
                            Monster.direction = Vector3FromAngle(0, 180);
                        }
                        else
                        {
                            Debug.Log("앞-오른쪽이 막히지않음(윗쪽), 우회전");
                            Monster.direction = Vector3FromAngle(90, 270);
                        }
                    }
                }
            }
        }
        // Debug.Log("Irreversible : " + Irreversible);
    }
}