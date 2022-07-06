using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurorialConversation : ActiveConversation
{
    public Image[] tutorialImg;
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

                SliderBackground.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                SliderImg.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                isChating = false;
                tutorialImg[0].gameObject.SetActive(false);
                tutorialImg[1].gameObject.SetActive(false);
                tutorialImg[2].gameObject.SetActive(false);
                tutorialImg[3].gameObject.SetActive(false);
                tutorialImg[4].gameObject.SetActive(false);
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
            SliderBackground.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            SliderImg.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                isChating = true;
                StartCoroutine(TextPractice());
            }

    }
    protected IEnumerator TextPractice()
    {
        string[] narrators = "위 키보드(w,a,s,d)를 누르면 캐릭터 이동, SHIFT키는 달리기입니다.$위 키보드(1,2,3,4,5)를 누르면 아이템사용, 열쇠는 아이템이 아닙니다.즉, 먹는 즉시 문이 열립니다. $스페이스바를 이용해 대화, 미션수행 등 여러 작업이 가능합니다.$돋보기가 나올 시 마우스를 이용해 선택이 가능합니다.".Split('$');
       
        foreach (string narrator in narrators)
        {
            if(narrator.Contains("캐릭터 이동"))
            {
                tutorialImg[0].gameObject.SetActive(true);
                tutorialImg[1].gameObject.SetActive(true);
            }
            else
            {
                tutorialImg[0].gameObject.SetActive(false);
                tutorialImg[1].gameObject.SetActive(false);

                tutorialImg[2].gameObject.SetActive(true);
                tutorialImg[3].gameObject.SetActive(true);
                tutorialImg[4].gameObject.SetActive(true);

            }
            yield return StartCoroutine(Chat("튜토리얼", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}
