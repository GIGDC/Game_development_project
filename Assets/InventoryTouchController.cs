using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryTouchController : MonoBehaviour, IPointerClickHandler
{
    GameObject target;
    
    public void OnPointerClick(PointerEventData eventData)
    {

        if (PlayerItemInteraction.Item.Count < 5)
        {
            target = eventData.pointerEnter;
        }
    }

    void Update()
    {
        if (target)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach (KeyCode key in PlayerItemInteraction.Item.Keys)
                {
                    
                    if (key != KeyCode.Alpha1)
                    {

                        Debug.Log("수정 수정, 인벤토리 수정");
                    }
                }
            }
        }

    }
}
