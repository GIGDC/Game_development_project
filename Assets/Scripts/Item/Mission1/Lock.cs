using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    string password; // �Է¹��� ��й�ȣ
    [Tooltip("�ùٸ� ��й�ȣ ��")]
    public string correctPassword;
    [Tooltip("1-3 ����")]
    [SerializeField] GameObject key;
    [SerializeField] InputField inputField;

    private void Start()
    {
        password = "";
        inputField.onValueChange.AddListener(ValueChanged);
    }

    void ValueChanged(string text)
    {
        password = text;
        Debug.Log(password);
    }

    private void Update()
    {
        if(password == correctPassword)
        {
            transform.gameObject.SetActive(false); // Ȯ��� �ڹ��� ����
            GameObject gameObject = GameObject.Find("GameObject");
            Transform[] childList = gameObject.GetComponentsInChildren<Transform>(true);
            foreach(Transform child in childList)
            {
                child.transform.gameObject.SetActive(true);
            }
            GameObject.Find("lock").GetComponent<Animator>().SetBool("unlock", true); // �ڹ��� ����
            GameObject.Find("lock").GetComponent<BoxCollider2D>().enabled = false; // �ڹ��� Ŭ�� ����
            key.SetActive(true);
        }
        else if(password.Length == 4)
        {
            Debug.Log("incorrect password");
            password = "";
            inputField.text = "";
            transform.gameObject.SetActive(false);
        }
    }
}
