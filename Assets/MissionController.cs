using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionController : MonoBehaviour, IPointerClickHandler
{
    GameObject target;
    public void OnPointerClick(PointerEventData eventData)
    {
       target = eventData.pointerEnter;
    }
    void Update()
    {
        if (target)
        {
            GameObject.Find("GameObject").GetComponentInChildren<Transform>().gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }
}
