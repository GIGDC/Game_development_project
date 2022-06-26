using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThreeConversation : ActiveConversation
{
    static public int GhostNum=0;
    public InputField ThreeMission;
    bool isQize;
    void Start()
    {
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
        ThreeMission = GameObject.FindWithTag("InputField").transform.GetComponent<InputField>();
        Debug.Log(ThreeMission);
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
                if (isQize)
                {
                    ThreeMission.GetComponent<InputField>().ActivateInputField();
                    ThreeMission.gameObject.SetActive(true);
                }
            }
        }

    }

    protected IEnumerator LoadTalk()
    {
        string narrator = "가사실로 가";
        yield return StartCoroutine(Chat("유령 " + id, narrator));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       
        if (collision.gameObject.name != "Player")
            return;

        if (ThreeMission == null)
            GameObject.FindWithTag("InputField").transform.GetComponent<InputField>();
        else
        if (ThreeMission.GetComponent<InputField>().gameObject.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                message.SetActive(true);
                clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                isChating = true;
                if (GhostNum < 3)
                {
                    StartCoroutine(LoadTalk());
                }
                else
                {
                    isQize = true;
                    StartCoroutine(Talk());
                }
            }
        }
           
    }
}
