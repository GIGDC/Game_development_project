using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStairMenu : MonoBehaviour
{
    GameObject stairMenu;
    private bool stairMenuReady;

    private void Start()
    {
        stairMenu = GameObject.Find("UI").transform.Find("StairMenu").gameObject;
        stairMenuReady = false;
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
                // Time.timeScale = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                stairMenu.SetActive(false);
                // Time.timeScale = 1f;
            }
        }
        else
        {
            stairMenu.SetActive(false);
        }
    }
}