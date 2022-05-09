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
                Debug.Log("UIŬ��");
                if (childList != null)
                {
                   // this.transform.gameObject.SetActive(false);
                    //�θ� ������Ʈ�� 0��°
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
