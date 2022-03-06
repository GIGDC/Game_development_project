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
    }

    // Start is called before the first frame update
    void Start()
    {
        messageReady = false;
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
