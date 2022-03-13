using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    private PlayerMovement thePlayer;  // �÷��̾ �ٶ󺸰� �ִ� ����
    private Vector2 vector;
    public GameObject on;

    private Quaternion rotation;  // ȸ��(����)�� ����ϴ� Vector4

    public GameObject monster;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();

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
        vector.Set(thePlayer.direction.x, thePlayer.direction.y);
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
