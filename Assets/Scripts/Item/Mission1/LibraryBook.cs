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
        if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("1-4 ø≠ºË") != null) // 1-4 ø≠ºË ¿ÃπÃ »πµÊ Ω√ √•¿« ∏∂¡ˆ∏∑ ∆‰¿Ã¡ˆ ∫∏ø©¡‹
            animator.SetBool("keyObtained", true); // √•¿« ∏∂¡ˆ∏∑ ∆‰¿Ã¡ˆ ∫∏ø©¡‹
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
            GameObject.Find("Player").GetComponent<PlayerMissionItem>().AddMissionItem("1-4 ø≠ºË");
            return;
        }
        animator.SetInteger("page", animator.GetInteger("page") + 1);
        Debug.Log("¥Ÿ¿Ω" + animator.GetInteger("page"));
    }

    public void PrevPage()
    {
        if(animator.GetInteger("page") < 1)
        {
            animator.SetInteger("page", 1);
            return;
        }
        animator.SetInteger("page", animator.GetInteger("page") - 1);
        Debug.Log("¿Ã¿¸" + animator.GetInteger("page"));
    }
}
