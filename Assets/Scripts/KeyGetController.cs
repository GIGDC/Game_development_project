using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyGetController : MonoBehaviour
{
    public GameObject child;
    GameObject target;

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
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            Debug.Log(target);
        }
    }
}
