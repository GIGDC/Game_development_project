using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThreeConversation : ActiveConversation
{
    static public int GhostNum=3;
    public InputField ThreeMission;
    bool isQize=false;
    static public bool isInputGame = false;
    static public bool isSuccess = false;
    void Start()
    {
        Setting();
        ThrowKey = false;
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
                Setting();
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
            isChating = true;
                    isQize = true;
                    StartCoroutine(Talk());
        }
          
    }
}
