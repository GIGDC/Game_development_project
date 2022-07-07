using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActivateStairMenu : MonoBehaviour
{
    GameObject floorUI;
    GameObject stairMenu;
    private bool stairMenuReady;
    private string currentSceneName;
    [Tooltip("씬 이동 후 플레이어 위치 설정")]
    [SerializeField] Vector3 teleportPosition;
    
    private void Start()
    {
        stairMenuReady = false;
        floorUI = GameObject.Find("Floor UI");
        stairMenu = floorUI.transform.Find("StairMenu").gameObject;
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
        if (stairMenuReady)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Space))
            {
                stairMenu.SetActive(true);
                floorUI.GetComponent<FloorUIManager>().TeleportPosition = teleportPosition;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                stairMenu.SetActive(false);
            }
        }
    }


}
