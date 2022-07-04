using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    private GameObject thePlayer;  // 플레이어가 바라보고 있는 방향
    private Vector2 vector;
    public GameObject on;

    private Quaternion rotation;  // 회전(각도)을 담당하는 Vector4

    public GameObject Lightning;
    int count = 0;

    public GameObject monster;
    SpriteRenderer sr;
    void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");
        if (mainCameras.Length == 1)
        {
            DontDestroyOnLoad(Lightning);
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(Lightning);
            Destroy(gameObject);
        } // 중복된 MainCamera 오브젝트가 있을 경우 오브젝트 파괴
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
            print("몬스터 등장");
            sr.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            print("몬스터 사라짐");
            sr.material.color = Color.clear;
        }
        
    }
}
