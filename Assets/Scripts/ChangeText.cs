using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    PlayerMovement player;
    public Text text;
    protected bool first; //ù��°�� true�� �Ǹ� �ѹ��� ������ ���ؼ�
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
        if (player.transform.position.y < -28 || !first)
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