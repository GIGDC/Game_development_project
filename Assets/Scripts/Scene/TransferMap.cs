using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public Animator sceneTransition;
    public float transitionTime = 1f;

    Animator doorAnim;
    public string transferMapName; // 이동할 맵의 이름
    [Tooltip("문의 방향 설정 (front, back, right, left)")]
    public string direction;
    Vector2 playerDirection; // 플레이어가 향하고 있는 방향
    RaycastHit2D MonsterCheck;

    static public bool CheckMonster=false;
    
    private void FixedUpdate()
    {
        MonsterCheck = Physics2D.Raycast(transform.position, Vector2.down, 3f, LayerMask.GetMask("Monster"));
        Debug.DrawRay(transform.position, Vector2.down * 3f, Color.red);

        if (MonsterCheck&&!MonsterTimer.OutDoor)
        {
            Vector3 v = transform.position- (Vector3)MonsterCheck.rigidbody.position;
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

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Monster")&&!MonsterTimer.OutDoor)
        {
            CheckMonster = true;
        }

        if (other.gameObject.name != "Player")
            return;

        playerDirection = GameObject.Find("Player").GetComponent<PlayerMovement>().GetDirectionNormalized();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Open");
            switch (direction)
            {
                case "front":
                    if (playerDirection.y > 0)
                        StartCoroutine(LoadMap(transferMapName));
                    break;
                case "back":
                    if (playerDirection.y < 0)
                        StartCoroutine(LoadMap(transferMapName));
                    break;
                case "right":
                    if (playerDirection.x > 0)
                        StartCoroutine(LoadMap(transferMapName));
                    break;
                case "left":
                    if (playerDirection.x < 0)
                        StartCoroutine(LoadMap(transferMapName));
                    break;
            }
        }
    }

    IEnumerator LoadMap(string transferMapName)
    {
        sceneTransition.SetTrigger("Start");
        doorAnim.SetBool("DoorOpen", true);
        yield return new WaitForSeconds(0.5f);

        doorAnim.SetBool("DoorOpen", false);
        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(transferMapName);
    }
}