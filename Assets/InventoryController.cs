using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private bool is_ActiveInven;
    Transform Inventory_E;

    void Start()
    {
        Inventory_E = GameObject.Find("UI").transform.Find("Inventory_E");
    }
    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.E))
            if (!is_ActiveInven)
            {
                Inventory_E.gameObject.SetActive(true);
                is_ActiveInven = true;
            }
            else
            {
                Inventory_E.gameObject.SetActive(false);
                is_ActiveInven = false;
            }

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
                if (is_ActiveInven)
                    if (key == KeyCode.LeftControl)
                    {
                        Inventory_E.GetChild(PlayerItemInteraction.Count).gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, PlayerItemInteraction.Item[key].Inven_Width);
                        Inventory_E.GetChild(PlayerItemInteraction.Count).gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, PlayerItemInteraction.Item[key].Inven_Height);
                        Inventory_E.GetChild(PlayerItemInteraction.Count).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        Inventory_E.GetChild(PlayerItemInteraction.Count).GetComponent<Image>().sprite = PlayerItemInteraction.Item[key].Img;
                    }
            }
        }

    }

}
