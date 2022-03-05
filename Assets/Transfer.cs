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
            return; //여기에 objcet 숨겨진 후 몇초 뒤에 등장하도록 구현 (ex: 몹)
       // manager.transferScene = "SampleScene";

        SceneTransition();

    }
    public void SceneTransition()
    {
        StartCoroutine(manager.LoadMap("SampleScene"));
    }
}