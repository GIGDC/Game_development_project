using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickHint : MonoBehaviour
{
    public Image child;
    GameObject target;
    static public bool Click = false;
    public int ImgNum;
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
         
            CastRay();

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
            Debug.Log(target);
        }
    }

}
