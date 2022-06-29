using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    public Image EventImage;
    GameObject target;
    public GameObject glass;
    bool isCollider = false;
    Transform[] childList;
    Animator Frige;
    private void Start()
    {
        childList = GameObject.Find("GameObject").GetComponentsInChildren<Transform>();
        Frige = EventImage.gameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        CastRay();
        if (isCollider && target.CompareTag("magnifiedObj"))
        {
            glass.SetActive(true);
            glass.transform.position = Input.mousePosition;

            Debug.Log(target);
            isCollider = false;
        }
        else
        {
            glass.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
           
            if (target == this.gameObject)
            {
                if (EventImage != null)
                 {
                    EventImage.gameObject.SetActive(true);
                    glass.SetActive(false);
                    if (childList != null)
                    {
                    //�θ� ������Ʈ�� 0��°
                    for (int i = 1; i < childList.Length; i++)
                    {
                        childList[i].gameObject.SetActive(false);
                        Debug.Log(childList[i].name);
                    }
                }
            }
            }
        }
    }
    public void CastRay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit;

        RaycastHit2D[] tmphit;
        hit = Physics2D.Raycast(pos, Vector2.zero, 0f,1<<LayerMask.NameToLayer("ItemLayout"));
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            isCollider = true;
            Debug.Log(target.name);
           
        }
    }
}
