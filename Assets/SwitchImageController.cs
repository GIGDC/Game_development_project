using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchImageController : MonoBehaviour
{
    GameObject target;
    public GameObject glass;
    bool isCollider = false;
    bool isClick = false;
    Transform inventory;
    public Sprite img;
    Selector[] ghost;
    bool isFirst = true;
    static bool isEating = false;
    private void Start()
    {
        int i = 0;
        inventory= GameObject.Find("UI").transform.Find("Inventory");
        ghost = FindObjectsOfType<Selector>();
    }
    void Update()
    {
        if(img)
            if (ThreeConversation.GhostNum >= 3)
            {
                if (isFirst){
                    for (int i = 0; i < 3; i++)
                    {
                        Destroy(ghost[i].gameObject);    
                    }
                    isFirst = false;
                }
                HandsController.isStarting = true; // 클릭으로 이동시켜야함.
                gameObject.GetComponent<SpriteRenderer>().sprite = img;
                isClick = true;
            }
    }

    private void FixedUpdate()
    {
        CastRay();
        // && target.CompareTag("magnifiedObj")
        if (isCollider)
        {
            glass.SetActive(true);
            glass.transform.position = Input.mousePosition;
            Debug.Log(target);
            isCollider = false;
            if (!isEating)
            {
                Destroy(this.gameObject);
                isEating = true;
            }
        }
        else
        {
            glass.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (target == this.gameObject && isClick)
            {
                glass.SetActive(false);
                PlayerItemInteraction.Click = this.gameObject;
                MaintainController.isEating = true;
                
            }
            isClick = false;
        }
    }
    public void CastRay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit;

        RaycastHit2D[] tmphit;
        hit = Physics2D.Raycast(pos, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("ItemLayout"));
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            isCollider = true;
            Debug.Log(target.name);

        }
    }

}
