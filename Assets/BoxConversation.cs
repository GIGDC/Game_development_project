using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxConversation : ActiveConversation
{
    public static bool isSuccess = false;
    void Start()
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
                isChating = false;
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
            yield return StartCoroutine(Chat("���� " + id, narrator));
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
            isChating = true;

            if (ThreeConversation.GhostNum < 3)
            {
                StartCoroutine(Talk("������ ����� ��, �ٵ� �� ���� �����ϴ� �����̸� �Ծ�.$ ����ǿ� �� ģ������ �����ž�, �³����� �� ������ �����.$"));
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
                StartCoroutine(Talk("����.. $ �ʰ� �ٶ���� �̰���? �̰� ������ ������ �ڸ� ã�ư���"));
                
            }
        }
    }
}
