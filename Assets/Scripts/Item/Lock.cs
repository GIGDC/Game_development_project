using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private string password; // 입력받을 비밀번호
    [Tooltip("올바른 비밀번호 값")]
    public string correctPassword;
    [Tooltip("1-3 열쇠")]
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
            transform.gameObject.SetActive(false); // 확대된 자물쇠 숨김
            GameObject gameObject = GameObject.Find("GameObject");
            Transform[] childList = gameObject.GetComponentsInChildren<Transform>(true);
            foreach(Transform child in childList)
            {
                child.transform.gameObject.SetActive(true);
            }
            GameObject.Find("lock").GetComponent<Animator>().SetBool("unlock", true); // 자물쇠 열림
            GameObject.Find("lock").GetComponent<BoxCollider2D>().enabled = false; // 자물쇠 클릭 방지
            key.SetActive(true);
        }
        else if(password.Length == 4)
        {
            Debug.Log("incorrect password");
            transform.gameObject.SetActive(false);
        }
    }
}
