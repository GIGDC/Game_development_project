using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SecConversation : ActiveConversation
{
    public GameObject SelectGame;
    public static bool isSuccess=false;
    bool isGaming = false;
    public static bool isCheck = false;
    public float lerpTime = 1.0f;
    public GameObject head;
    public GameObject Audio;
    public AudioClip dropSound;
    private void Start()
    {
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
    }
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
                isChating = false;
                if (isGaming)
                    SelectGame.SetActive(true);
                if (!isGaming && isCheck)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        if (isSuccess)
        {
            StartCoroutine(lerpCoroutine(head.transform.position, new Vector3(head.transform.position.x, -600, 0), lerpTime));
        }

        if (isCheck && isGaming)
        {
            message.SetActive(true);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
            StartCoroutine(QuizTalk(isSuccess));
            head.SetActive(false);
            Audio.GetComponent<AudioSource>().clip = dropSound;
            Audio.GetComponent<AudioSource>().Play();
            isGaming = false;
            isSuccess = false;
        }

    }
    IEnumerator lerpCoroutine(Vector2 current, Vector2 target, float time)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);

            head.transform.position
                = Vector3.Lerp(current, target, elapsedTime / time);

            yield return null;
        }
        head.transform.position = target;

        yield return null;
        isCheck = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Id = id;
        if (collision.gameObject.name != "Player")
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            messageImg.sprite = img;
            message.SetActive(true);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
            isGaming = true;
            StartCoroutine(Talk());
        }
    }
}
