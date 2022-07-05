using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    AudioSource audioP;
    CameraShake shake;
    ActiveConversation ghost;
    public Text textfield;
    public Image clap;
    public Image ele1;
    float scaleSpeed = 1f;
    bool check = false;
    public GameObject audio;
    public GameObject message;
    public Image messageImg;
    public Sprite img;
    public AudioClip ac;
    // Update is called once per frame

    //���� �����Ҹ� -> �ڼ��� �ٲٱ�
    static int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioP = audio.GetComponent<AudioSource>();
        ghost = GameObject.FindObjectOfType<ActiveConversation>();
        shake = GameObject.FindObjectOfType<CameraShake>();
        messageImg.sprite = img;
        audioP.clip = ac;
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
                clap.gameObject.SetActive(true);
                audioP.Play();
                
                ThreeConversation.isSuccess = true;
                Debug.Log("����");
            }
            else
            {
                textfield.text = "";
                count++;
                if (count > 2)
                {
                    // PlayerAttacted.hp -= 90;
                    message.SetActive(true);
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
                ThreeConversation.isSuccess = false;
                ThreeConversation.isInputGame = true;
                this.gameObject.SetActive(false);

            }
        }
    }
}
