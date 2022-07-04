using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeText : MonoBehaviour
{
    GameObject player;
    public Text text;
    protected bool first; //첫번째로 true가 되면 한번만 실행을 위해서
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
                text.text = "후관";
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
                text.text = "본관";
                first = true;
            }
        }
        else if (SceneName == "Auditorium")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "강당";
            //  first = true;
        }
        else if (SceneName == "Start Room")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "튜토리얼";
            // first = true;
        }
        else if (SceneName == "Admin office")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "행정실";
            // first = true;
        }
        else if (SceneName == "Broadcasting")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "방송실";
            // first = true;
        }
        else if (SceneName == "Library")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "도서실";
            // first = true;
        }
        else if (SceneName == "Principals' office")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "교장실";
        }
        else if (SceneName == "Special Ed")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "도움반";
            // first = true;
        }
        else if (SceneName == "Storage")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "경비실";
            // first = true;
        }
        else if (SceneName == "FirstAnnex")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "별관 복도";
            // first = true;
        }
        else if (SceneName == "Cooking&Technology")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "가사실";
            // first = true;
        }
        else if (SceneName.Contains("Men Toilet"))
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "남자화장실";
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
