using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer_60 : MonoBehaviour
{

    private float min; //������ 60��������, ���� üũ�� ���ؼ� �ʴ����� ����
    private float sec; //�� ����
    private float timerSpeed = 1.0f; //Ÿ�̸� ���ǵ� 
    private float timer;
    public bool isStop = false;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * timerSpeed;
        DisplayTime();

        if (isStop)
        {
            Reset();
        }
    }

    void DisplayTime()
    {

        if (timer >= 61.0f)
        {
            timer -= 60.0f;
            isStop = true;
        }
        /*
        if (timer >= 60.0f * 60.0f)
        {
            timer -= 60.0f * 60.0f;
            //isStop = true;
        }
        */
        int hours = Mathf.FloorToInt(timer / (60.0f * 60.0f));
        int minutes = Mathf.FloorToInt(timer / 60.0f - hours * 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60 - hours * 60.0f * 60.0f);

        if (hours > 12)
            hours -= 12;

        // transform.localEulerAngles=new Vector3(0,0,minutes/60.0f*-360.0f);
        transform.localEulerAngles = new Vector3(0, 0, seconds / 60.0f * -360.0f);
    }

    public void Reset()
    {
        timer = 0;
    }

}