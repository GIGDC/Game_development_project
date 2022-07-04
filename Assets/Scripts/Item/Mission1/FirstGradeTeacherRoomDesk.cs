using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGradeTeacherRoomDesk : MonoBehaviour
{
    Animator lockAnimator;
    GameObject drawer;

    void Start()
    {
        drawer = transform.Find("교무실책상_서랍 아랫칸").gameObject;
        lockAnimator = drawer.transform.Find("lock").GetComponent<Animator>();
    }


    void Update()
    {
        if(lockAnimator.GetCurrentAnimatorStateInfo(0).IsName("UnlockLock")) // 자물쇠가 풀리면 서랍 열림
        {
            drawer.GetComponent<Animator>().SetTrigger("open");
        }
    }
}
