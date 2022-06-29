using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThreeConversation : ActiveConversation
{
    static public int GhostNum=0;
    public GameObject ThreeGhost;
    public InputField ThreeMission;
    bool isQize;
    static public bool isSuccess = false;
    void Start()
    {
        ThrowKey = false;
        clock = GameObject.Find("Clock").GetComponent<Image>();
        secondHand = GameObject.Find("theMinuteHand").GetComponent<Image>();
        Debug.Log(ThreeMission);
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
                if (isQize)
                {
                    ThreeMission.GetComponent<InputField>().ActivateInputField();
                    ThreeMission.gameObject.SetActive(true);
                }
            }

            if (ThreeGhost != null && isSuccess)
                ThreeGhost.gameObject.SetActive(false);
        }

    }

    protected IEnumerator LoadTalk()
    {
        string narrator = "�谡 ����..\n ����ǿ� ����, ���� �����ϴ� �����̸� ������.";
        yield return StartCoroutine(Chat("���� " + id, narrator));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));

    }
    
    public IEnumerator PlayerTalk()
    {
        string narrator = "��ᰡ �ִ�. \n�̰�... ���� ����ϱ�?";
        yield return StartCoroutine(Chat("Player", narrator));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name != "Player")
            return;

        if (ThreeMission != null) {
            if (ThreeMission.GetComponent<InputField>().gameObject.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    message.SetActive(true);
                    clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    isChating = true;
                    if (GhostNum < 3)
                    {
                        StartCoroutine(LoadTalk());
                    }
                    else
                    {
                        isQize = true;
                        foreach (KeyCode key in PlayerItemInteraction.Item.Keys)
                        {
                            if (PlayerItemInteraction.Item[key].Name == "Tteokbokki")
                            {
                                PlayerItemInteraction.Item.Remove(key);
                                PlayerItemInteraction.Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                                break;
                            }
                        }
                        StartCoroutine(Talk());
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isSuccess)
                {
                    message.SetActive(true);
                    clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    isChating = true;
                    foreach (KeyCode key in PlayerItemInteraction.Item.Keys)
                    {
                        if (PlayerItemInteraction.Item[key].Name == "Box")
                        {
                            PlayerItemInteraction.Item.Remove(key);
                            PlayerItemInteraction.Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                            break;
                        }
                    }
                    StartCoroutine(SuccessTalk());

                }
            }
        }

           
    }
}
