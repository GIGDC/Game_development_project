using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    AudioSource audioP;
    CameraShake shake;
    ActiveConversation ghost;
    Text textfield;
    Image clap;
    Image ele1;
    float scaleSpeed = 1f;
    bool check = false;
    public GameObject audio;
    //���� �����Ҹ� -> �ڼ��� �ٲٱ�
    static int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioP = audio.GetComponent<AudioSource>();
        ghost = GameObject.FindObjectOfType<ActiveConversation>();
        shake = GameObject.FindObjectOfType<CameraShake>();
        textfield = GameObject.Find("UI").transform.FindChild("InputField").transform.Find("Text").gameObject.GetComponent<Text>();
        clap= GameObject.Find("UI").transform.FindChild("Clap").gameObject.GetComponent<Image>();
        ele1 = GameObject.Find("UI").transform.FindChild("ele1").gameObject.GetComponent<Image>();
    }

    void ScaleUP()
    {
        ele1.gameObject.SetActive(false);
        Debug.Log("2�ʵ� ����?");

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
                audioP.Play();
                Debug.Log("����");
            }
            else
            {
                textfield.text = "";
                count++;
                if (count > 2)
                {
                   // PlayerAttacted.hp -= 90;

                    ele1.gameObject.SetActive(true);
                    Invoke("ScaleUP", 2);

                    if (PlayerAttacted.hp < 0)
                    {
                        Debug.Log("���ӿ���");
                    }
                    this.gameObject.SetActive(false);
                }
                else
                {
                    shake.Shake();
                }
            }
        }
    }
}
