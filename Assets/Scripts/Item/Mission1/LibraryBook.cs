using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryBook : MonoBehaviour
{
    Button obtainKeyBtn;
    Animator animator;


    void Start()
    {
        obtainKeyBtn = transform.Find("obtain_key").GetComponent<Button>();
        animator = GetComponent<Animator>();
        obtainKeyBtn.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("1-4 ø≠ºË") != null) // 1-4 ø≠ºË ¿ÃπÃ »πµÊ Ω√ √•¿« ∏∂¡ˆ∏∑ ∆‰¿Ã¡ˆ ∫∏ø©¡‹
            animator.SetInteger("page", 3);

        if (Input.GetMouseButtonDown(0) && animator.GetInteger("page") == 1)
            animator.SetInteger("page", 2);

        if (animator.GetInteger("page") == 2) 
            obtainKeyBtn.enabled = true;  
    }
}
