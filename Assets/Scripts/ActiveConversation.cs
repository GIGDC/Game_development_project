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
    public static Dictionary<int, Ghost> ghost;
    public bool ThrowKey;

    protected InputField ThreeMission; //3번째미션용, 없으면 null을 반납함
    //Image sr;

    //start부분을 메소드 오버라이딩 되면, 다 다시 할당해줘야하기때문에, 최상위 클래스에 배치
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
        ThreeMission = GameObject.Find("UI").transform.Find("InputField").gameObject.transform.GetComponent<InputField>(); //InputField겨져오기
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
        string[] narrators = ghost[id].Talk.Split('$');
        foreach(string narrator in narrators)
        {
            yield return StartCoroutine(Chat("유령 " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }

    protected IEnumerator TextPractice()
    {
        yield return StartCoroutine(Chat("학생1", "이것은 타이핑 효과를 통해 대사창을 구현하는 연습"));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Chat("학생2", "안녕하세요, 반갑습니다."));
    }
}