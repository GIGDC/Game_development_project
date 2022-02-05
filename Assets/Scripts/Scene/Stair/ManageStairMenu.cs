using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageStairMenu : MonoBehaviour
{
    public static bool stairMenuOpened = false;
    public GameObject stairMenuUI;

    private void Start()
    {
        stairMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        stairMenuUI.SetActive(true);
        Time.timeScale = 0f;
        stairMenuOpened = true;
    }

    public void CloseMenu()
    {
        stairMenuUI.SetActive(false);
        Time.timeScale = 1f;
        stairMenuOpened = false;
    }
}
