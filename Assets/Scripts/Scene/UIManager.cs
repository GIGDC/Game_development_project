using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : GameManager
{
    GameObject stairMenu;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); // memory leak
        stairMenu = transform.Find("StairMenu").gameObject;
    }

    void Update()
    {
        if(stairMenu.activeSelf)
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
                        = new Vector3(0f, -78, 0);
                    thirdFloorBtn.gameObject.SetActive(false);
                    break;
                case "2F":
                    firstFloorBtn.SetActive(true);
                    firstFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(0f, 35, 0);
                    secondFloorBtn.gameObject.SetActive(false);
                    thirdFloorBtn.SetActive(true);
                    thirdFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(0f, -78, 0);
                    break;
                case "3F":
                    firstFloorBtn.gameObject.SetActive(false);
                    secondFloorBtn.SetActive(true);
                    secondFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(0f, -78, 0);
                    thirdFloorBtn.gameObject.SetActive(false);
                    break;
                default:
                    firstFloorBtn.SetActive(true);
                    firstFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(0f, 35, 0);
                    secondFloorBtn.SetActive(true);
                    secondFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(0f, -78, 0);
                    thirdFloorBtn.SetActive(true);
                    thirdFloorBtn.GetComponent<RectTransform>().anchoredPosition
                        = new Vector3(0f, -191, 0);
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
        transitionAnimator = GetTransitionAnimator();
        transferScene = sceneName;
        StartCoroutine(FadeOut());
    }

    public void ActivateStairMenu(bool b)
    {
        stairMenu.SetActive(b);
    }
}
