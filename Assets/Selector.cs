using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Selector : MonoBehaviour
{
    public GameObject select;
    public string[] text;
    public int num;

    private void OnCollisionStay2D(Collision2D collision)
    {

        SelectController.GhostSelect = num;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            select.gameObject.SetActive(true);
            GameObject.Find("Select01").transform.Find("Text").GetComponent<Text>().text=text[0];
            GameObject.Find("Select02").transform.Find("Text").GetComponent<Text>().text=text[1];
        }
    }
}
