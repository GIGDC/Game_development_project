using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button btn;

    // Update is called once per frame
    void Start()
    {
        btn.onClick.AddListener(Event);

    }

    void Event()
    {
        Debug.Log("��ư�� ���Ƚ��ϴ�.");
        this.gameObject.SetActive(false);
    }
}
