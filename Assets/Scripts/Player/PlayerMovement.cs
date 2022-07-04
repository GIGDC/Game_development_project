using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement player;
    public static string CurrentMapName; //현재 맵은 무엇인가.
    Rigidbody2D rigid;
    public Animator animator;
    public float speed;
    public float normalSpeed, crawlSpeed, runSpeed;
    Vector2 movement;
    public Vector2 direction; // 플레이어가 현재 향하고 있는 방향
    public bool isAttacking; //플레이어가 공격할때 당하는 모션을 끄기 위해서

    public AudioClip[] walkPlayer;
    AudioSource walkSource;
    private void Start()
    {

        //사운드 조절
       //d walkSource = GetComponent<AudioSource>();
       // walkSource.volume = 0.2f;

    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } // 중복된 Player 오브젝트가 있을 경우 오브젝트 파괴

        rigid = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
       
            //if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            //StartCoroutine("WalkSound");

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (movement.sqrMagnitude > 0)
            {
                direction.x = Input.GetAxisRaw("Horizontal");
                direction.y = Input.GetAxisRaw("Vertical");
            }

            ChangeMoveSpeed();
            if (animator.GetFloat("MoveSpeed") > 0)
            {
                // 이동

                animator.SetFloat("MoveHorizontally", movement.x);
                animator.SetFloat("MoveVertically", movement.y);
            }
        
    }

    void FixedUpdate()
    {

        rigid.MovePosition(rigid.position + movement * speed * Time.deltaTime);
    }
    IEnumerator WalkSound()
     {
            walkSource.clip = walkPlayer[0];
            walkSource.Play();
            yield return new WaitForSeconds(0.5f);
            if (!walkSource.isPlaying)
            {
                walkSource.clip = walkPlayer[1];
                walkSource.Play();
                yield return new WaitForSeconds(2.0f);
            }
        
    }

    // 키 입력에 따른 이동 속도 변경
    void ChangeMoveSpeed()
    {
        // 기어가기
        if (Input.GetKey(KeyCode.C))
            speed = crawlSpeed;
        // 뛰기
        else if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        // 일반 속도
        else
            speed = normalSpeed;

        animator.SetFloat("MoveSpeed", movement.sqrMagnitude * speed);
        animator.speed = speed / 4; // 이동 속도의 4분의 1만큼의 빠르기로 애니메이션 재생
    }

    public Vector2 GetDirectionNormalized()
    {
        return direction.normalized;
    }
}