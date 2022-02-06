using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageStairMenu : MonoBehaviour
{
    GameObject stairMenu;

    private void Start()
    {
        stairMenu = GameObject.Find("StairMenu");
        stairMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        stairMenu.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        stairMenu.SetActive(false);
    }
}
