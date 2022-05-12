using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    CameraShake shake;
    ActiveConversation ghost;
    Text textfield;
    Image clap;
    static int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.FindObjectOfType<ActiveConversation>();
        shake = GameObject.FindObjectOfType<CameraShake>();
        textfield = GameObject.Find("InputField").transform.Find("Text").gameObject.GetComponent<Text>();
        clap= GameObject.Find("UI").transform.Find("Clap").gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (textfield.text.Contains("박수"))
            {
                this.gameObject.SetActive(false);
                ghost.transform.gameObject.SetActive(false); //귀신삭제
                clap.gameObject.SetActive(true);
                Debug.Log("정답");
            }
            else
            {
                textfield.text = "";
                Debug.Log("오답");
                shake.Shake();
                count++;

                if (count > 3)
                {
                    PlayerAttacted.hp -= 90;
                    if (PlayerAttacted.hp < 0)
                    {
                        Debug.Log("게임오버");
                    }
                    Debug.Log("10미만일시 게임오버");
                }
            }
        }
    }
}
