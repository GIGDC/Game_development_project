using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGradeTeacherRoomDesk : MonoBehaviour
{
    Animator lockAnimator;
    GameObject drawer;

    void Start()
    {
        drawer = transform.Find("������å��_���� �Ʒ�ĭ").gameObject;
        lockAnimator = drawer.transform.Find("lock").GetComponent<Animator>();
    }


    void Update()
    {
        if(lockAnimator.GetCurrentAnimatorStateInfo(0).IsName("UnlockLock")) // �ڹ��谡 Ǯ���� ���� ����
        {
            drawer.GetComponent<Animator>().SetTrigger("open");
        }
    }
}
