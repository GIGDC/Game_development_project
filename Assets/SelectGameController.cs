using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectGameController : MonoBehaviour
{
    GameObject click = null;
    public GameObject eye;
    Slider slider;
    protected void Setting()
    {
        slider = GameObject.Find("Hp").GetComponent<Slider>();
        slider.value = (float)PlayerAttacted.hp / (float)PlayerAttacted.maxHP;
    }
    private void Start()
    {
        Setting();
    }
    public void OnClick()
    {
        click = EventSystem.current.currentSelectedGameObject;
        if (click.name.Contains("me"))
        {
            PlayerAttacted.hp -= 5;
            FirstConversation.SelectNum = 3;
            eye.SetActive(false);
        }
        else if (click.name.Contains("you"))
        {
            FirstConversation.SelectNum = 2;
        }
        else if (click.name.Contains("ghost"))
        {
            PlayerAttacted.hp -= 5;
            FirstConversation.SelectNum = 1;
            eye.SetActive(false);
        }
        this.gameObject.SetActive(false);
        FirstConversation.isSelect = true;
    }
}
