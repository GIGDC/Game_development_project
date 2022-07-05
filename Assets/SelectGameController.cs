using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectGameController : MonoBehaviour
{
    GameObject click = null;
    public GameObject eye;
    public void OnClick()
    {
        click = EventSystem.current.currentSelectedGameObject;

        if (click.name.Contains("me"))
        {
            FirstConversation.SelectNum = 3;
            eye.SetActive(false);
        }
        else if (click.name.Contains("you"))
        {
            FirstConversation.SelectNum = 2;
        }
        else if (click.name.Contains("ghost"))
        {
            FirstConversation.SelectNum = 1;
            eye.SetActive(false);
        }
        this.gameObject.SetActive(false);
        FirstConversation.isSelect = true;
    }
}
