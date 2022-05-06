using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer_60 : MonoBehaviour
{

    private float timerSpeed = 1.0f; //타이머 스피드 
    public static float timer=3000f;
    public static bool isStop = false;
    // Update is called once per frame
    static int hours;
    static int minutes;
    static int seconds;
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

        if (timer >= 60.0f * 60.0f)
        {
            timer -= 60.0f * 60.0f;
            isStop = true;
        }
        hours = Mathf.FloorToInt(timer / (60.0f * 60.0f));
        minutes = Mathf.FloorToInt(timer / 60.0f - hours * 60);
        seconds = Mathf.FloorToInt(timer - minutes * 60 - hours * 60.0f * 60.0f);

        if (hours > 12)
            hours -= 12;

        transform.localEulerAngles=new Vector3(0,0,minutes/60.0f*-360.0f);
        //Debug.Log(minutes);
    }

    public void Reset()
    {
        timer = 0;
    }

}