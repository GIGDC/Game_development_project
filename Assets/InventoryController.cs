using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        if (PlayerItemInteraction.Item != null)
        {
            foreach (KeyCode key in PlayerItemInteraction.Item.Keys)
            {
                
                if (key <= KeyCode.Alpha5)
                {
                    transform.GetChild((int)key - 49).GetChild(0).gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, PlayerItemInteraction.Item[key].Width);
                    transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().sprite = PlayerItemInteraction.Item[key].Img;
                }
            }
        }

    }

}
