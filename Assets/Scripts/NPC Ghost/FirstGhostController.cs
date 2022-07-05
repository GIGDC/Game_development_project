using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class FirstGhostController : ActiveConversation
{
    static bool isSaving = false;
    public GameObject Select;
    public GameObject DeLight;
    public GameObject SaLight;
    public GameObject Audio;
    public AudioClip ac;

    GameObject Click;

    // Start is called before the first frame update
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

                if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("½ÇÆÈÂî") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("ÆíÁö") != null)
                {
                    SaLight.SetActive(true);
                    this.gameObject.SetActive(false);
                    HandsController.isStarting = false;
                }
            }
        }
    }
    public void onClick()
    {
        Click = EventSystem.current.currentSelectedGameObject;

        if (Click.name.Contains("±¸¿ø"))
        {
            isSaving = true;
        }
        else
        {
            isSaving = false;
            DeLight.SetActive(true);
            Audio.GetComponent<AudioSource>().clip = ac;
            Audio.GetComponent<AudioSource>().Play();
            this.gameObject.SetActive(false);
        }
        Select.SetActive(false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            messageImg.sprite = img;
            if ((GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("½ÇÆÈÂî") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("ÆíÁö") != null) 
            || isSaving)
            {
                message.SetActive(true);
                clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                isChating = true;
                StartCoroutine(SelectTalk());
            }
            else
            {
                Select.SetActive(true);
            }
        }
    }
    protected IEnumerator SelectTalk()
    {
        string[] narrators = null;
        if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("½ÇÆÈÂî") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("ÆíÁö") != null)
        {
            StartCoroutine(Talk());
        }
        else
        {
            if (isSaving)
                narrators = "³» ½ÇÆÈÂî¿Í ÆíÁö¸¦ ÀÒ¾î¹ö·È¾î...! ±×°ÍµéÀ» Ã£¾ÆÁà!!$".Split('$');
        }

        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("ÇÐ»ý 3", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}
