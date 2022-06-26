using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SelectController : MonoBehaviour
{
    CameraShake shake;
    Selector[] select;
    public GameObject audio;
    public GameObject Btn;
    public bool click = false;
    // Start is called before the first frame update
    private void Start()
    {
        select= FindObjectsOfType<Selector>();
        shake = FindObjectOfType<CameraShake>();
       
    }

    public void PointerDown()
    {
        Btn = null;
        click = true;
    }
    public void Update()
    {
        if (click)
        {
            Btn = EventSystem.current.currentSelectedGameObject;
            string text = Btn.GetComponentInChildren<Text>().text;
            Debug.Log(text);
            if (text.Contains("π–") || text.Contains("X") || text.Contains("±√¡ﬂ∂±∫∫¿Ã"))
            {
                foreach (Selector s in select)
                {
                    if (s.getCheck())
                    {
                        ThreeConversation.GhostNum++;
                        Debug.Log("¡¯«‡¡ﬂ");
                        audio.GetComponent<AudioSource>().Play();
                        s.setCheck(false);
                        this.gameObject.SetActive(false);
                        s.gameObject.SetActive(false);
                        s.but1.SetActive(false);
                        s.but2.SetActive(false);
                    }
                }
            }
            else
            {
                shake.Shake();
                PlayerAttacted.hp -= 15;
            }
            click = false;
        }
    }
}
