using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxConversation : ActiveConversation
{
    public static bool isSuccess = false;
    void Start()
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
                isChating = false;

                SliderBackground.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                SliderImg.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                if (isSuccess)
                    this.gameObject.SetActive(false);
            }
        }
    }
    protected IEnumerator Talk(string line)
    {
        string[] narrators = line.Split('$');
        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("유령 " + id, narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            messageImg.sprite = img;
            message.SetActive(true);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
            SliderBackground.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            SliderImg.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

            if (ThreeConversation.GhostNum < 3)
            {
                StartCoroutine(Talk("떡볶이 만들어 와, 근데 난 내가 좋아하는 떡볶이만 먹어.$ 가사실에 내 친구들이 있을거야, 걔네한테 내 취향을 물어봐.$"));
            }
            else
            {
                foreach (KeyCode key in PlayerItemInteraction.Item.Keys)
                {
                    if (PlayerItemInteraction.Item[key].Name == "Tteokbokki")
                    {
                        PlayerItemInteraction.Item.Remove(key);
                        PlayerItemInteraction.Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                        break;
                    }
                }
                isSuccess = true;
                StartCoroutine(Talk("좋아.. $ 너가 바라던게 이거지? 이걸 가지고 구원할 자를 찾아가봐"));
                
            }
        }
    }
}
