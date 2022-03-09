using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transfer : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return; //���⿡ objcet ������ �� ���� �ڿ� �����ϵ��� ���� (ex: ��)
       // manager.transferScene = "SampleScene";

        SceneTransition();

    }
    public void SceneTransition()
    {
        StartCoroutine(manager.LoadMap("SampleScene"));
    }
}