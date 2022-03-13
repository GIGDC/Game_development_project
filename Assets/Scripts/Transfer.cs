using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transfer : MonoBehaviour
{
    GameManager manager;
    public string GoTo;
    public string direction;
    StartPoint start;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.FindObjectOfType<StartPoint>();
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return; //���⿡ objcet ������ �� ���� �ڿ� �����ϵ��� ���� (ex: ��)
                    // manager.transferScene = "SampleScene";

        StartPoint.MapNum = SceneManager.GetActiveScene().buildIndex;

        if (direction != "")
        {
            StartPoint.direction = direction;
        }
        SceneTransition();

    }
    public void SceneTransition()
    {
        StartCoroutine(manager.LoadMap(GoTo));
    }
}