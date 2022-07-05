using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeText : MonoBehaviour
{
    GameObject player;
    public Text text;
    protected bool first; //ù��°�� true�� �Ǹ� �ѹ��� ������ ���ؼ�
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player"); 
        text = GetComponent<Text>();
        first = false;
    }

    // Update is called once per frame
    void Update()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        
        if (SceneName == "1F" || SceneName == "2F" || SceneName == "3F")
        {
            if (player.transform.position.y < -28 && !first)
            {
                StartCoroutine(FadeInCoroutine());
                text.text = "�İ�";
                first = true;
            }
            if (player.transform.position.y > -28 && player.transform.position.y < 0.5f)
            {
                first = false;
                StartCoroutine(FadeOutCoroutine());
                text.text = "";

            }
            if (player.transform.position.y > 0.5f)
            {
                StartCoroutine(FadeInCoroutine());
                text.text = "����";
                first = true;
            }
        }
        else if (SceneName == "Auditorium")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "����";
            //  first = true;
        }
        else if (SceneName == "Start Room")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "Ʃ�丮��";
            // first = true;
        }
        else if (SceneName == "Admin office")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "������";
            // first = true;
        }
        else if (SceneName == "Broadcasting")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "��۽�";
            // first = true;
        }
        else if (SceneName == "Library")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "������";
            // first = true;
        }
        else if (SceneName == "Principals' office")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "�����";
        }
        else if (SceneName == "Special Ed")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "�����";
            // first = true;
        }
        else if (SceneName == "Storage")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "����";
            // first = true;
        }
        else if (SceneName == "FirstAnnex")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "���� ����";
            // first = true;
        }
        else if (SceneName == "Cooking&Technology")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "�����";
            // first = true;
        }
        else if (SceneName.Contains("Men Toilet"))
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "����ȭ���";
            // first = true;
        }
    }

    protected IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 0;

        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.1f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, fadeCount);
        }
    }

    protected IEnumerator FadeInCoroutine()
    {
        float fadeCount = 1;

        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.1f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, fadeCount);
        }
    }

}
