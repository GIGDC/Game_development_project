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
            return; //여기에 objcet 숨겨진 후 몇초 뒤에 등장하도록 구현 (ex: 몹)
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
