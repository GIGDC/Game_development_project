using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    RaycastHit2D hit;
    public GameObject MagGlass;
    bool isCollider = false;
    GameObject target;

    private void FixedUpdate()
    {
        CastRay();
        Vector3 position = MagGlass.transform.localPosition;
        if (target)
        {
            position.x = target.transform.localPosition.x;
            position.y = target.transform.localPosition.y;
        }
        MagGlass.transform.localPosition = position;
    }
    public void CastRay() {
        Debug.Log(hit.collider.name);
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;

            Debug.Log(target.name);
            isCollider = true;
        }
    }
}
