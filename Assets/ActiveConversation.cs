using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveConversation : MonoBehaviour
{

    public GameObject message;
    private bool messageReady;

    public Text chatText;  // ���� ä���� ������ �ؽ�Ʈ
    public Text CharacterName;  // ĳ���� �̸��� ������ �ؽ�Ʈ

    Image clock;
    Image secondHand;
    //Image sr;

    // Start is called before the first frame update
    void Start()
    {
        messageReady = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
        //sr = clock.GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        print("npc ����");
        messageReady = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        print("npc ��������");

        messageReady = false;
        message.SetActive(false);
        //sr.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clock.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        secondHand.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (messageReady)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("��ȭ ����");
                message.SetActive(true);
                //sr.material.color = Color.clear;
                clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

                StartCoroutine(TextPractice());
            }
        }
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        string writerText = "";

        // �ؽ�Ʈ Ÿ���� ȿ��
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            chatText.text = writerText;
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("�л�1", "�̰��� Ÿ���� ȿ���� ���� ���â�� �����ϴ� ����"));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(NormalChat("�л�2", "�ȳ��ϼ���, �ݰ����ϴ�."));
    }
}
