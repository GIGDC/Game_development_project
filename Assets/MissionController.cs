using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionController : MonoBehaviour
{
    GameObject target;
    Transform[] childList;
    void start()
    {
        childList = GameObject.Find("GameObject").GetComponentsInChildren<Transform>();
    }
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(EventSystem.current.IsPointerOverGameObject());
            if (EventSystem.current.IsPointerOverGameObject()==this.gameObject)
            {
                Debug.Log("UI클릭");
                if (childList != null)
                {
                   // this.transform.gameObject.SetActive(false);
                    //부모 오브젝트가 0번째
                    for (int i = 1; i < childList.Length; i++)
                    {
                        childList[i].gameObject.SetActive(true);
                        Debug.Log(childList[i].name);
                    }
                }
            }
          
        }
    }
}
