using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickHint : MonoBehaviour
{
    public Image child;
    GameObject target;
    static public bool Click = false;
    public int ImgNum;
    public GameObject glass;
    bool isHint = false;
    bool isCollider = false;

    private void FixedUpdate()
    {
        CastRay();
        if (isCollider && isHint)
        {
            Vector3 position = glass.transform.localPosition;
            position.x = target.transform.localPosition.x;
            position.y = target.transform.localPosition.y;
            glass.transform.localPosition = position;

            print(position);
            glass.SetActive(true);
            Debug.Log(target);
            isCollider = false;
            isHint = false;
        }
        else
        {
            glass.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (target == this.gameObject)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void CastRay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero,0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            isCollider = true;
            if(hit.collider.tag == "hint")
            {
                isHint = true;
            }
        }
    }
}
