using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

abstract public class ActiveConversation : MonoBehaviour
{
    public GameObject message;
    public GameObject Key;
    public Text chatText;  // ���� ä���� ������ �ؽ�Ʈ
    public Text CharacterName;  // ĳ���� �̸��� ������ �ؽ�Ʈ
    public int id;
    protected Image clock;
    protected Image secondHand;
    protected bool isChating;
    public static Dictionary<int, Ghost> ghost;
    public bool ThrowKey;

    protected InputField ThreeMission; //3��°�̼ǿ�, ������ null�� �ݳ���
    //Image sr;

    //start�κ��� �޼ҵ� �������̵� �Ǹ�, �� �ٽ� �Ҵ�������ϱ⶧����, �ֻ��� Ŭ������ ��ġ
    // Start is called before the first frame update
    public int Id
    {
        set { id = value; }
    }
    void Start()
    {
        ghost = new Dictionary<int, Ghost>();
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
        ThreeMission = GameObject.Find("UI").transform.Find("InputField").gameObject.transform.GetComponent<InputField>(); //InputField��������
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
                //ThreeMission.gameObject.SetActive(true);
                //this.gameObject.SetActive(false);
               
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
             clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
             secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
             isChating = true;
             StartCoroutine(Talk());
         }

    }
    protected IEnumerator Chat(string narrator, string narration)
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
    protected IEnumerator Talk()
    {
        string[] narrators = ghost[id].Talk.Split('$');
        foreach(string narrator in narrators)
        {
            yield return StartCoroutine(Chat("���� " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }

    protected IEnumerator TextPractice()
    {
        yield return StartCoroutine(Chat("�л�1", "�̰��� Ÿ���� ȿ���� ���� ���â�� �����ϴ� ����"));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Chat("�л�2", "�ȳ��ϼ���, �ݰ����ϴ�."));
    }
}