using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveConversation : MonoBehaviour
{
    public GameObject message;
    private bool messageReady;

    public Text chatText;  // 실제 채팅이 나오는 텍스트
    public Text CharacterName;  // 캐릭터 이름이 나오는 텍스트

    Image clock;
    Image secondHand;

    public bool ThrowKey;
    //Image sr;

    // Start is called before the first frame update
    void Start()
    {
        //messageReady = false;
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
        //sr = clock.GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        print("npc 만남");
        //messageReady = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        print("npc 빠져나감");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ThrowKey = true;
            messageReady = false;
            message.SetActive(false);
            //sr.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("대화 시작");
            message.SetActive(true);
            //sr.material.color = Color.clear;
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

            StartCoroutine(TextPractice());
        }

    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        string writerText = "";

        // 텍스트 타이핑 효과
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            chatText.text = writerText;
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("학생1", "이것은 타이핑 효과를 통해 대사창을 구현하는 연습"));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(NormalChat("학생2", "안녕하세요, 반갑습니다."));
    }
}
