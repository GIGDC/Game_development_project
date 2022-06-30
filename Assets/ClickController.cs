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

    private void Start()
    {
        childList = GameObject.Find("GameObject").GetComponentsInChildren<Transform>();
    }
    private void FixedUpdate()
    {
        CastRay();
        if (isCollider && target.CompareTag("magnifiedObj"))
        {
            glass.SetActive(true);
            glass.transform.position = Input.mousePosition;
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
                EventImage.gameObject.SetActive(true);
                glass.SetActive(false);
                if (childList != null)
                {
                    //부모 오브젝트가 0번째
                    for (int i = 1; i < childList.Length; i++)
                    {
                        childList[i].gameObject.SetActive(false);
                    }
                }
               
            }
        }
    }

    public void CastRay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            isCollider = true;
        }
    }
}
