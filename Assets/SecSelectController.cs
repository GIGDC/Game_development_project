using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SecSelectController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject click = null;
    public void OnClick()
    {
        click = EventSystem.current.currentSelectedGameObject;

        if (click.name.Contains("4"))
        {
            SecConversation.isSuccess = true;

        }
        else
        {
            SecConversation.isCheck = true;
           SecConversation.isSuccess = false;
        }
        this.gameObject.SetActive(false);
    } 
   
}
