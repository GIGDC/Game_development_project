using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstConversation : ActiveConversation
{
    public GameObject CupGame;
    public GameObject SelectGame;
    public static bool isSuccess = false;
    public static bool isSelect = false;
    bool isCupGaming = false;
    bool isSelectGaming = false;
    public static int SelectNum = -1;
    private void Start()
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
                clock.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                isChating = false;
                if (!isSuccess && !isSelect)
                {
                    isCupGaming = true;
                    CupGame.SetActive(true);
                }
                else if (isSuccess && !isSelect)
                {
                    isCupGaming = false;
                    isSelectGaming = true;
                    SelectGame.SetActive(true);
                }

                if (isSuccess && isSelect)
                {
                    message.SetActive(false);
                    Destroy(this.gameObject);
                }
            }
        }

        if (isSuccess&&isCupGaming)
        {
            message.SetActive(true);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
            StartCoroutine(PositTalk());
        }
        if (isSelect&&isSelectGaming)
        {
            message.SetActive(true);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
            StartCoroutine(SelectTalk(SelectNum));
            isSelectGaming = false;
        }

       
    }
    protected IEnumerator SelectTalk(int n)
    {
        string[] narrators = null;
        if (n == 1)
            narrators = DataController.ghosts[id].Select1.Split('$');
        else if (n == 2)
            narrators = DataController.ghosts[id].Select2.Split('$');
        else
            narrators = DataController.ghosts[id].Select3.Split('$');

        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("���� " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            messageImg.sprite = img;
            message.SetActive(true);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
            StartCoroutine(Talk());
        }
    }
}
