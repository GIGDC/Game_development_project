using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LockController : MonoBehaviour
{
    const string passwd = "4257";
    Button[] Objects;
    GameObject click;
    string input = "";
    bool[] isClick;
    public static bool isLock;

    // Start is called before the first frame update
    void Start()
    {
        Objects = this.gameObject.GetComponentsInChildren<Button>();
        isClick = new bool[Objects.Length+1];
        isLock = false;

        for (int i = 0; i < Objects.Length + 1; i++)
            isClick[i] = false;
    }
    public void OnClick()
    {

        int index = -1;
        if (input == passwd)
        {
            this.gameObject.SetActive(false);
            isLock = true;
        }
        if (input.Length >= 4 && passwd!=input)
        {
            input = "";
            foreach (Button Object in Objects)
            {
                index = Int32.Parse(Object.gameObject.name) - 1;
                isClick[index] = false;
                Object.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
        }
        click=EventSystem.current.currentSelectedGameObject;
        foreach (Button Object in Objects)
        {
            if (click.gameObject.name == Object.name && !isClick[Int32.Parse(click.gameObject.name) - 1])
            {
                input += click.gameObject.name;
                Object.GetComponent<Image>().color = new Color(255, 255, 255, 100);
                index = Int32.Parse(click.gameObject.name) - 1;
                isClick[index] = true;
                
            }
        }

        Debug.Log("입력된 비밀번호 :" + input+" "+index.ToString() );
    }
}
