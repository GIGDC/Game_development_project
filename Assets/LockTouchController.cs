using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LockTouchController : MonoBehaviour
{

    GameObject target;

    public void OnPointerClick(PointerEventData eventData)
    {

        if (PlayerItemInteraction.Item.Count < 5)
        {
            target = eventData.pointerEnter;
            print(target);
        }
    }

    private void FixedUpdate()
    {
        CastRay();

        if (Input.GetMouseButtonDown(0))
        {
            if (target == this.gameObject)
            {
                print("버튼 클릭");
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
