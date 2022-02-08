using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStairMenuUI : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Find("UI").gameObject.SetActive(true);
            // Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.Find("UI").gameObject.SetActive(false);
            // Time.timeScale = 1f;
        }
    }
}
