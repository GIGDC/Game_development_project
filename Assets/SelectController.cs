using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SelectController : MonoBehaviour
{

    CameraShake shake;
    Selector[] select;
    public static int GhostSelect;
    // Start is called before the first frame update
    private void Start()
    {
        select= FindObjectsOfType<Selector>();
        shake = FindObjectOfType<CameraShake>();
    }
    public void SelectBtn()
    {
        GameObject Btn = EventSystem.current.currentSelectedGameObject;
        string text=Btn.GetComponentInChildren<Text>().text;
        if (text.Contains("π–∂±") || text.Contains("æÓπ¨X") || text.Contains("±√¡ﬂ∂±∫∫¿Ã"))
        {
            this.gameObject.SetActive(false);
            if (select[0].num==GhostSelect)
            {
                select[0].gameObject.SetActive(false);
            }else if(select[1].num == GhostSelect)
            {
                select[1].gameObject.SetActive(false);
            }
            else
            {
                select[2].gameObject.SetActive(false);
            }
        }
        else
        {
            shake.Shake();
            PlayerAttacted.hp -= 15;
        }
    }
}
