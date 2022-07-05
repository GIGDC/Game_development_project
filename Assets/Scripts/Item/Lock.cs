using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private string password; // �Է¹��� ��й�ȣ
    [Tooltip("�ùٸ� ��й�ȣ ��")]
    public string correctPassword;
    [Tooltip("1-3 ����")]
    [SerializeField] GameObject key;

    private void Start()
    {
        password = "";
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
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
            transform.gameObject.SetActive(false);
        }
    }
}
