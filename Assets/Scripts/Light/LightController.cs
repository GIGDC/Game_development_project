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

    public GameObject Lightning;
    int count = 0;

    public GameObject monster;
    SpriteRenderer sr;
    void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");
        if (lights.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Light()
    {
        Lightning.transform.position = this.transform.position;
        count = 0;
        StartCoroutine(ShowReady());
    }
    IEnumerator ShowReady()
    {
        while (count < 3)
        {
            Lightning.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Lightning.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            count++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        Lightning.SetActive(false);
        InvokeRepeating("Light", 97f, 100f);
        thePlayer = GameObject.FindGameObjectWithTag("Player");

        monster = GameObject.FindGameObjectWithTag("Monster");

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
