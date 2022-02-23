using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToRoom : GameManager
{
    Animator doorAnim;
    [Tooltip("문의 방향 설정 (front, back, right, left)")]
    public string direction;
    RaycastHit2D MonsterCheck;

    static public bool CheckMonster = false;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
        transferScene = "Classroom";
    }

    private void FixedUpdate()
    {
        MonsterCheck = Physics2D.Raycast(transform.position, Vector2.down, 3f, LayerMask.GetMask("Monster"));
        Debug.DrawRay(transform.position, Vector2.down * 3f, Color.red);

        if (MonsterCheck && !MonsterTimer.OutDoor)
        {
            Vector3 v = transform.position - (Vector3)MonsterCheck.rigidbody.position;
            Monster.direction = Vector3FromAngle(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
            Debug.Log(Monster.direction);
        }
    }

    //같은 함수는 하나의 클래스에 묶어서 관리하도록 정리할 예정.
    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Monster") && !MonsterTimer.OutDoor)
        {
            CheckMonster = true;
        }

        if (other.gameObject.name != "Player")
            return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Open");
            StartCoroutine(FadeOut());
        }
    }

    override protected IEnumerator FadeOut()
    {
        transitionAnimator = GetTransitionAnimator();
        transitionAnimator.SetBool("FadeOut", true);
        transitionAnimator.SetBool("FadeIn", false);
        doorAnim.SetBool("DoorOpen", true);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(transitionTime);
        doorAnim.SetBool("DoorOpen", false);
        doorAnim.SetBool("DoorClose", false);
        StartCoroutine(AsyncLoadMap());
        yield return null;
    }
}
