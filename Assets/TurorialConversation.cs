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

        Debug.Log("npc ���� 22");
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
        string[] narrators = "�� Ű����(w,a,s,d)�� ������ ĳ���� �̵�, SHIFTŰ�� �޸����Դϴ�.$�� Ű����(1,2,3,4,5)�� ������ �����ۻ��, ����� �������� �ƴմϴ�.��, �Դ� ��� ���� �����ϴ�. $�����̽��ٸ� �̿��� ��ȭ, �̼Ǽ��� �� ���� �۾��� �����մϴ�.$�����Ⱑ ���� �� ���콺�� �̿��� ������ �����մϴ�.".Split('$');
       
        foreach (string narrator in narrators)
        {
            if(narrator.Contains("ĳ���� �̵�"))
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
            yield return StartCoroutine(Chat("Ʃ�丮��", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}
