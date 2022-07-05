using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseItemImage : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject gameObject = GameObject.Find("GameObject");
            Transform[] childList = gameObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childList)
            {
                child.transform.gameObject.SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }
}
