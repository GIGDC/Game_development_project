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
            if (textfield.text.Contains("�ڼ�"))
            {
                this.gameObject.SetActive(false);
                ghost.transform.gameObject.SetActive(false); //�ͽŻ���
                clap.gameObject.SetActive(true);
                Debug.Log("����");
            }
            else
            {
                textfield.text = "";
                Debug.Log("����");
                shake.Shake();
                count++;

                if (count > 3)
                {
                    PlayerAttacted.hp -= 90;
                    if (PlayerAttacted.hp < 0)
                    {
                        Debug.Log("���ӿ���");
                    }
                    Debug.Log("10�̸��Ͻ� ���ӿ���");
                }
            }
        }
    }
}
