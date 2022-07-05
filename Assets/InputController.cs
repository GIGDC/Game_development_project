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

    //음악 웃음소리 -> 박수로 바꾸기
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
        Debug.Log("2초뒤 실행?");

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (textfield.text.Contains("박수"))
            {
                this.gameObject.SetActive(false);
                clap.gameObject.SetActive(true);
                audioP.Play();
                
                ThreeConversation.isSuccess = true;
                Debug.Log("정답");
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
                        Debug.Log("게임오버");
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
