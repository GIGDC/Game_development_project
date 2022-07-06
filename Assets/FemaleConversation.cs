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
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();

        SliderBackground = GameObject.Find("Background").GetComponent<Image>();
        SliderImg = GameObject.Find("Fill").GetComponent<Image>();

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

                SliderBackground.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                SliderImg.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                isChating = false;

                if (isSuccess) //미션2 
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

        if (Click.name.Contains("구원"))
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
                clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                SliderBackground.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                SliderImg.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
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
            narrators = "맞아.. $얘가 나보고 체육창고로 오라 그랬었는데 막상 가 보니 얘는 없고 체육창고 문은 잠겨버렸었지..$ 순찰 중인 경비아저씨 덕에 가까스로 나올 수 있었어.$".Split('$');
        }
        else
        {
            if (isSaving)
                narrators = "체육관에 있는 열쇠를 찾아서 나에게 전달해줘...!$ 내꺼를 찾아줘!!!".Split('$');
        }

        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("학생 2", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}
