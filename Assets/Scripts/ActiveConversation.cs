using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

abstract public class ActiveConversation : MonoBehaviour
{
    public GameObject message;
    public GameObject Key;
    public Text chatText;  // 실제 채팅이 나오는 텍스트
    public Text CharacterName;  // 캐릭터 이름이 나오는 텍스트
    public int id;
    protected Image clock;
    protected Image secondHand;
    protected bool isChating;
    public bool ThrowKey;
    public Image messageImg;
    public Sprite img;
    //Image sr;

    //start부분을 메소드 오버라이딩 되면, 다 다시 할당해줘야하기때문에, 최상위 클래스에 배치
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

        Debug.Log("npc 만남 22");
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

        // 텍스트 타이핑 효과
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
            yield return StartCoroutine(Chat("유령 " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
    protected IEnumerator PositTalk()
    {
        string[] narrators = DataController.ghosts[id].Posit.Split('$');
        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("유령 " + id, narrator));
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
            yield return StartCoroutine(Chat("유령 " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }

    }
    protected IEnumerator SuccessTalk()
    {
        string[] narrators = DataController.ghosts[id].Success.Split('$');
        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("유령 " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
    //
    //위 키보드를 누르면 캐릭터 이동, SHIFT키는 달리기입니다.$위 키보드를 누르면 아이템사용, 열쇠는 아이템이 아닙니다. 즉, 먹는 즉시 문이 열립니다.$돋보기가 나올 시 마우스 사용 가능합니다.
    protected IEnumerator TextPractice()
    {
        string[] narrators = "위 키보드를 누르면 캐릭터 이동, SHIFT키는 달리기입니다.$위 키보드를 누르면 아이템사용, 열쇠는 아이템이 아닙니다. 즉, 먹는 즉시 문이 열립니다.$돋보기가 나올 시 마우스 사용 가능합니다.".Split('$');
        
        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("튜토리얼", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}