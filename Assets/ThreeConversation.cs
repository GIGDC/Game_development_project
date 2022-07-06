using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThreeConversation : ActiveConversation
{
    static public int GhostNum=0;
    public InputField ThreeMission;
    bool isQize=false;
    static public bool isInputGame = false;
    static public bool isSuccess = false;
    void Start()
    {
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();

        SliderBackground = GameObject.Find("Background").GetComponent<Image>();
        SliderImg = GameObject.Find("Fill").GetComponent<Image>();
        Debug.Log(ThreeMission);
    }
    void Update()
    {
        if (MaintainController.isMission2[0])
            Destroy(this);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isChating)
            {
                if (Key != null)
                    Key.SetActive(true);
                message.SetActive(false);
                clock.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                SliderBackground.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                SliderImg.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                if (isQize)
                {
                    ThreeMission.gameObject.SetActive(true);
                }
                else
                {
                    MaintainController.isMission2[0] = true;
                    this.gameObject.SetActive(false);
                }
                isChating = false;
            }   
        }
        if (isInputGame&&isQize)
        {
            message.SetActive(true);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;

            StartCoroutine(QuizTalk(isSuccess));
            isQize = false;
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

            SliderBackground.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            SliderImg.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
                    isQize = true;
                    StartCoroutine(Talk());
        }
          
    }
}
