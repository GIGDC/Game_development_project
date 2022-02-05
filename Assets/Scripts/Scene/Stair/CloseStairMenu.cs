using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStairMenu : MonoBehaviour
{
    public static bool stairMenuOpened;
    public GameObject stairMenuUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CloseMenu();
        }
    }

    void CloseMenu()
    {
        stairMenuUI.SetActive(false);
        Time.timeScale = 1f;
        stairMenuOpened = false;
    }
}
