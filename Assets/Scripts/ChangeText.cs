using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeText : MonoBehaviour
{
    PlayerMovement player;
    public Text text;
    protected bool first; //첫번째로 true가 되면 한번만 실행을 위해서
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        text = GetComponent<Text>();
        first = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (player.transform.position.y < -28 || !first)
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
        else if(SceneManager.GetActiveScene().name == "Auditorium")
        {
            StartCoroutine(FadeInCoroutine());
            text.text = "강당";
            first = true;
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
