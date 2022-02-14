using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateStairMenuUI : MonoBehaviour
{
    GameObject ui;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ui = transform.Find("UI").gameObject;
            ui.SetActive(true);

            Image image=ui.GetComponent<Image>(); // 알파 설정
            Color color = image.color;
            color.a = 1;
            image.color = color;
            // Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.Find("UI").gameObject.SetActive(false);
            // Time.timeScale = 1f;
        }
    }
}
