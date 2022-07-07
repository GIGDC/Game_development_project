using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazebarController : MonoBehaviour
{
    public Slider slider;
    public float time = 99;
    public float maxTime = 100;
    public Image canvas;
    public Text TimerText;
    public float LimitTime;
    public Text explain;
    int isSuccess = 0;
    int isFail = 0;
    public GameObject closeKeyStorage;
    public GameObject openKeyStorage;
    public Image glass;

    public Image Space;
    public Sprite SpaceOff;
    public Sprite SpaceOn;
    public GameObject Key;
    public AudioSource Audio;
    public Image Monster;
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        time = 99;
        maxTime = 100;
        LimitTime = 15;
        Audio.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스페이스 누름");
            time += (float)6;
            Space.sprite = SpaceOn;
        }
        else
        {
            Space.sprite = SpaceOff;
        }

        if(isSuccess == 0)
        {
            time -= (float)0.5;
            slider.value = (float)time / maxTime;

            LimitTime -= Time.deltaTime;
            TimerText.text = Mathf.Round(LimitTime).ToString() + "s";
        }

        if (time == 0 || LimitTime <= 0)
        {
            explain.text = "실패입니다.....";
            isFail = 1;
            Monster.gameObject.SetActive(true);
            monster.gameObject.SetActive(true);
            TimerText.gameObject.SetActive(false);
            StartCoroutine(deleayTime());
        }
        if (time == 100)
        {
            Key.SetActive(true);
            explain.text = "열쇠 보관함 문이 열립니다......";
            isSuccess = 1;
            StartCoroutine(deleayTime());
            Audio.Stop();
            closeKeyStorage.gameObject.SetActive(false);
            glass.gameObject.SetActive(false);
            openKeyStorage.gameObject.SetActive(true);
            TimerText.gameObject.SetActive(false);
        }

    }

    IEnumerator deleayTime()
    {
        yield return new WaitForSeconds(3);
        canvas.gameObject.SetActive(false);

        if (isFail != 0)
        {
            time = 99;
            maxTime = 100;
            LimitTime = 15;
            isFail = 0;
            explain.text = "스페이스!!!!!!!!!!!!";
            Monster.gameObject.SetActive(false); 
            TimerText.gameObject.SetActive(true);
        }
    }
}
