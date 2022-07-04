using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement player;
    public static string CurrentMapName; //���� ���� �����ΰ�.
    Rigidbody2D rigid;
    public Animator animator;
    public float speed;
    public float normalSpeed, crawlSpeed, runSpeed;
    Vector2 movement;
    public Vector2 direction; // �÷��̾ ���� ���ϰ� �ִ� ����
    public bool isAttacking; //�÷��̾ �����Ҷ� ���ϴ� ����� ���� ���ؼ�

    public AudioClip[] walkPlayer;
    AudioSource walkSource;
    private void Start()
    {

        //���� ����
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
        } // �ߺ��� Player ������Ʈ�� ���� ��� ������Ʈ �ı�

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
                // �̵�

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

    // Ű �Է¿� ���� �̵� �ӵ� ����
    void ChangeMoveSpeed()
    {
        // ����
        if (Input.GetKey(KeyCode.C))
            speed = crawlSpeed;
        // �ٱ�
        else if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        // �Ϲ� �ӵ�
        else
            speed = normalSpeed;

        animator.SetFloat("MoveSpeed", movement.sqrMagnitude * speed);
        animator.speed = speed / 4; // �̵� �ӵ��� 4���� 1��ŭ�� ������� �ִϸ��̼� ���
    }

    public Vector2 GetDirectionNormalized()
    {
        return direction.normalized;
    }
}