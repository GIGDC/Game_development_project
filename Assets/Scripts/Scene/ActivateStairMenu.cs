using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActivateStairMenu : MonoBehaviour
{
    //GameObject UI;
    GameObject stairMenu;
    private bool stairMenuReady;
    private string currentSceneName;

    private void Start()
    {
        stairMenuReady = false;
        stairMenu = GameObject.Find("UI").transform.Find("StairMenu").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        stairMenuReady = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;
        stairMenuReady = false;
        stairMenu.SetActive(false);
    }

    private void Update()
    {
        if(stairMenuReady)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                stairMenu.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                stairMenu.SetActive(false);
            }
        }
    }

    
}
