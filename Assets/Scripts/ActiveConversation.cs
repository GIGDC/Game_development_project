using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ActiveConversation : MonoBehaviour
{
    public GameObject message;
    public GameObject Key;
    public Text chatText;  // ���� ä���� ������ �ؽ�Ʈ
    public Text CharacterName;  // ĳ���� �̸��� ������ �ؽ�Ʈ
    public int id;
    Image clock;
    Image secondHand;
    bool isChating;
    public static Dictionary<int, Ghost> ghost;
    public bool ThrowKey;
    //Image sr;

    // Start is called before the first frame update
    void Start()
    {
        ghost = new Dictionary<int, Ghost>();
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;
        Debug.Log("Escape");
        
        //messageReady = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isChating)
            {
                if (Key != null)
                    Key.SetActive(true);
                message.SetActive(false);
                //sr.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                clock.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                isChating = false;
               
                this.gameObject.SetActive(false);
               
            }
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log("npc ���� 22");
        if (collision.gameObject.name != "Player")
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            message.SetActive(true);
            //sr.material.color = Color.clear;
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
            StartCoroutine(Talk());
        }

    }
    /**/
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
    IEnumerator Chat(string narrator, string narration)
    {
        string writerText = "";
        CharacterName.text = narrator;

        // �ؽ�Ʈ Ÿ���� ȿ��
        for (int a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            chatText.text = writerText;
            yield return null;
        }

    }
    IEnumerator Talk()
    {
        string[] narrators = ghost[id].Talk.Split('$');
        foreach(string narrator in narrators)
        {
            yield return StartCoroutine(Chat("���� " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("�л�1", "�̰��� Ÿ���� ȿ���� ���� ���â�� �����ϴ� ����"));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(NormalChat("�л�2", "�ȳ��ϼ���, �ݰ����ϴ�."));
    }
}