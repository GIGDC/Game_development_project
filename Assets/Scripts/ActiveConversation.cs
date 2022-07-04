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
    public bool ThrowKey;
    public Image messageImg;
    public Sprite img;
    //Image sr;

    //start�κ��� �޼ҵ� �������̵� �Ǹ�, �� �ٽ� �Ҵ�������ϱ⶧����, �ֻ��� Ŭ������ ��ġ
    // Start is called before the first frame update
    public int Id
    {
        set { id = value; }
    }
    void Start()
    {
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
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
        string[] narrators = DataController.ghosts[id].Talk.Split('$');
        foreach(string narrator in narrators)
        {
            yield return StartCoroutine(Chat("���� " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
    protected IEnumerator PositTalk()
    {
        string[] narrators = DataController.ghosts[id].Posit.Split('$');
        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("���� " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }


    protected IEnumerator QuizTalk(bool isSuccess)
    {
        string[] narrators = null;

        if (isSuccess)
        {
            narrators = DataController.ghosts[id].Posit.Split('$');
        }
        else
            narrators = DataController.ghosts[id].Negat.Split('$');

        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("���� " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }

    }
    protected IEnumerator SuccessTalk()
    {
        string[] narrators = DataController.ghosts[id].Success.Split('$');
        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("���� " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
    //
    //�� Ű���带 ������ ĳ���� �̵�, SHIFTŰ�� �޸����Դϴ�.$�� Ű���带 ������ �����ۻ��, ����� �������� �ƴմϴ�. ��, �Դ� ��� ���� �����ϴ�.$�����Ⱑ ���� �� ���콺 ��� �����մϴ�.
    protected IEnumerator TextPractice()
    {
        string[] narrators = "�� Ű���带 ������ ĳ���� �̵�, SHIFTŰ�� �޸����Դϴ�.$�� Ű���带 ������ �����ۻ��, ����� �������� �ƴմϴ�. ��, �Դ� ��� ���� �����ϴ�.$�����Ⱑ ���� �� ���콺 ��� �����մϴ�.".Split('$');
        
        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("Ʃ�丮��", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}