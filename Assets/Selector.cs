using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Selector : MonoBehaviour
{
    public GameObject select;
    public string[] text;
    bool check = false;
    public GameObject but1;
    public GameObject but2;

    public bool getCheck()
    {
        return check;
    }
    public void setCheck(bool b)
    {
        this.check = b;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            check = true;
            select.gameObject.SetActive(true);
            but1.gameObject.SetActive(true);
            but2.gameObject.SetActive(true);

            but1.transform.Find("Text").GetComponent<Text>().text=text[0];
            but2.transform.Find("Text").GetComponent<Text>().text=text[1];
        }
    }
}