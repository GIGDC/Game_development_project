using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToCorridor : MonoBehaviour
{
    GameManager manager;
    private bool enterReady;

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

        enterReady = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;
        enterReady = false;
    }

    private void Update()
    {
        if (enterReady)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Open");
                StartCoroutine(manager.LoadMap("1F"));
            }
        }
    }
}
