using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveConversation : MonoBehaviour
{
    public GameObject message;
    public GameObject Key;
    public Text chatText;  // ���� ä���� ������ �ؽ�Ʈ
    public Text CharacterName;  // ĳ���� �̸��� ������ �ؽ�Ʈ
<<<<<<< HEAD
    
    Image clock;
    Image secondHand;

    public static Dictionary<string, Ghost> ghost;
=======
    public int id;
    Image clock;
    Image secondHand;

    public static Dictionary<int, Ghost> ghost;
>>>>>>> feature/#53-Inventory
    public bool ThrowKey;
    //Image sr;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        ActiveConversation.ghost = new Dictionary<string, Ghost>();
=======
        ghost = new Dictionary<int, Ghost>();
>>>>>>> feature/#53-Inventory
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;
        print("npc ����");
        //messageReady = true;
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

            StartCoroutine(Talk());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Key!=null)
                Key.SetActive(true);
            message.SetActive(false);
            //sr.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            this.gameObject.SetActive(false);
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
        for (int a=0; a < narration.Length; a++)
        {   writerText += narration[a];
            chatText.text = writerText;
            yield return null;
        }

    }
    IEnumerator Talk() {
<<<<<<< HEAD
        string[] narrators = ghost["���� 0"].Talk.Split('$');
        foreach (string narrator in narrators) {
            yield return StartCoroutine(Chat("���� 0",narrator));
=======

        string[] narrators = ghost[id].Talk.Split('$');
        foreach (string narrator in narrators) {
            yield return StartCoroutine(Chat("���� "+id,narrator));
>>>>>>> feature/#53-Inventory
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("�л�1", "�̰��� Ÿ���� ȿ���� ���� ���â�� �����ϴ� ����"));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(NormalChat("�л�2", "�ȳ��ϼ���, �ݰ����ϴ�."));
    }
}
