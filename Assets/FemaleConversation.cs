using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class FemaleConversation : ActiveConversation
{
    static bool isSaving = false;
    public GameObject Select;
    public GameObject DeLight;
    public GameObject SaLight;
    public GameObject Audio;
    public AudioClip ac;
    public static bool isSuccess=false;
    GameObject Click;

    // Start is called before the first frame update
    private void Start()
    {
        Setting();
        ThrowKey = false;

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
                
                Setting();
                isChating = false;

                if (isSuccess) //?̼?2 
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

        if (Click.name.Contains("????"))
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
            if (isSuccess || isSaving)
            {
                message.SetActive(true);
                isChating = true;

                foreach (KeyCode key in PlayerItemInteraction.Item.Keys)
                {
                    if (PlayerItemInteraction.Item[key].Name == "StorageKey")
                    {
                        PlayerItemInteraction.Item.Remove(key);
                        PlayerItemInteraction.Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                        break;
                    }
                }
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
        if (isSuccess)
        {
            narrators = "?¾?.. $?갡 ?????? ü??â???? ???? ?׷????µ? ???? ?? ???? ???? ???? ü??â?? ???? ???ܹ??Ⱦ???..$ ???? ???? ?????????? ???? ????? ???? ?? ?־???.$".Split('$');
        }
        else
        {
            if (isSaving)
                narrators = "?????Ƿ? ????,$ ?ű⿡ ?????? ?߾??? ?־? ??????, ?????? ?? ?????ʾ?.. $".Split('$');
        }

        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("?л? 2", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}
