using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FloorUIManager : MonoBehaviour
{
    GameManager gameManager;
    GameObject stairMenu;
    Animator transitionAnimator;
    [Tooltip("씬 이동 후 플레이어 위치 설정")]
    Vector3 teleportPosition;

    void Start()
    {
        GameObject[] floorUIs = GameObject.FindGameObjectsWithTag("floorUI");
        if (floorUIs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        stairMenu = transform.Find("StairMenu").gameObject;
        stairMenu.SetActive(false);
        teleportPosition = default(Vector3);
    }

    void Update()
    {
        if (stairMenu.activeSelf)
        {
            Pause();
            GameObject firstFloorBtn = stairMenu.transform.Find("BrushStroke").Find("1F").gameObject;
            GameObject secondFloorBtn = stairMenu.transform.Find("BrushStroke").Find("2F").gameObject;
            GameObject thirdFloorBtn = stairMenu.transform.Find("BrushStroke").Find("3F").gameObject;

            string currentSceneName = SceneManager.GetActiveScene().name;
            switch (currentSceneName)
            {
                case "1F":
                    firstFloorBtn.SetActive(false);
                    secondFloorBtn.SetActive(true);
                    secondFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(-300, 5, 0);
                    thirdFloorBtn.gameObject.SetActive(false);
                    break;
                case "2F":
                    firstFloorBtn.SetActive(true);
                    firstFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(-260, 72, 0);
                    secondFloorBtn.gameObject.SetActive(false);
                    thirdFloorBtn.SetActive(true);
                    thirdFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(-260, -87, 0);
                    thirdFloorBtn.GetComponent<Button>().enabled = false; // 현재 3층 씬 없음
                    break;
                case "3F":
                    firstFloorBtn.gameObject.SetActive(false);
                    secondFloorBtn.SetActive(true);
                    secondFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(-300, 5, 0);
                    thirdFloorBtn.gameObject.SetActive(false);
                    break;
                default:
                    firstFloorBtn.SetActive(true);
                    firstFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(-260, 140, 0);
                    secondFloorBtn.SetActive(true);
                    secondFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(-300, 5, 0);
                    thirdFloorBtn.SetActive(true);
                    thirdFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(-300, -130, 0);
                    break;
            }
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void SceneTransition(string sceneName)
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        transitionAnimator = gameManager.GetTransitionAnimator();
        gameManager.transferScene = sceneName;
        StartCoroutine(gameManager.FadeOut(teleportPosition));
    }

    public void ActivateStairMenu(bool b)
    {
        stairMenu.SetActive(b);
    }

    public Vector3 TeleportPosition
    {
        get { return teleportPosition; }
        set { teleportPosition = value; }
    }
}
