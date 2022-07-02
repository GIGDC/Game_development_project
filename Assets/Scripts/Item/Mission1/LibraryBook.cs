using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1Book : MonoBehaviour
{
    Button obtainKeyBtn;
    Animator animator;
    bool keyObtained;

    void Start()
    {
        obtainKeyBtn = transform.Find("obtain_key").GetComponent<Button>();
        animator = GetComponent<Animator>();
        keyObtained = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && animator.GetInteger("page") == 1)
            animator.SetInteger("page", 2);

        if (animator.GetInteger("page") == 2) 
            obtainKeyBtn.enabled = true;
        else
            obtainKeyBtn.enabled = false;

        if(keyObtained)
            animator.SetInteger("page", 3);
    }

    public bool KeyObtained
    { 
        get { return keyObtained; } 
        set { keyObtained = value; }
    }
}
