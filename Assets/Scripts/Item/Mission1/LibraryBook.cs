using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryBook : MonoBehaviour
{
    Animator animator;
    Button nextPageBtn;
    Button prevPageBtn;
    Text text;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("page", 1);
        //nextPageBtn = transform.Find("nextPage").GetComponent<Button>();
        ////nextPageBtn.onClick.AddListener(NextPage);
        //prevPageBtn = transform.Find("prevPage").GetComponent<Button>();
        //nextPageBtn.onClick.AddListener(PrevPage);
        text = transform.Find("Text").GetComponent<Text>();
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("1-4 ����") != null) // 1-4 ���� �̹� ȹ�� �� å�� ������ ������ ������
            animator.SetBool("keyObtained", true); // å�� ������ ������ ������
    }

    public void NextPage()
    {
        if (animator.GetInteger("page") > 7)
        {
            if (animator.GetBool("keyObtained") == true)
            {
                this.gameObject.SetActive(false);
                return;
            }
            
            if(animator.GetBool("keyObtained") == false)
                text.gameObject.SetActive(true);
            else 
                text.gameObject.SetActive(false);
            animator.SetBool("keyObtained", true);
            GameObject.Find("Player").GetComponent<PlayerMissionItem>().AddMissionItem("1-4 ����");
            return;
        }
        animator.SetInteger("page", animator.GetInteger("page") + 1);
        Debug.Log("����" + animator.GetInteger("page"));
    }

    public void PrevPage()
    {
        if(animator.GetInteger("page") < 1)
        {
            animator.SetInteger("page", 1);
            return;
        }
        animator.SetInteger("page", animator.GetInteger("page") - 1);
        Debug.Log("����" + animator.GetInteger("page"));
    }
}
