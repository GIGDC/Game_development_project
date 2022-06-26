using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    private GameObject thePlayer;  // �÷��̾ �ٶ󺸰� �ִ� ����
    private Vector2 vector;
    public GameObject on;

    private Quaternion rotation;  // ȸ��(����)�� ����ϴ� Vector4

    public GameObject monster;
    SpriteRenderer sr;

    void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");
        if (mainCameras.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } // �ߺ��� MainCamera ������Ʈ�� ���� ��� ������Ʈ �ı�
    }

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");

        monster = GameObject.Find("Monster");

        if (monster != null)
        {
            sr = monster.GetComponent<SpriteRenderer>();
            sr.material.color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = thePlayer.transform.position;
        vector.Set(thePlayer.transform.position.x, thePlayer.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            print("���� ����");
            sr.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            print("���� �����");
            sr.material.color = Color.clear;
        }
    }
}
