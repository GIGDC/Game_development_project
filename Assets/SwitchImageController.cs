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
                HandsController.isStarting = true; // 클릭으로 이동시켜야함.
                gameObject.GetComponent<SpriteRenderer>().sprite = img;
                isClick = true;
            }
    }

    private void FixedUpdate()
    {
        if (ThreeConversation.GhostNum >= 3)
        {
            CastRay();
            if (isEating)
            {
                Destroy(this.gameObject);
            }
            else
            {
                // && target.CompareTag("magnifiedObj")
                if (isCollider && target.CompareTag("Item"))
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

                    if (target == this.gameObject && isClick)
                    {
                        glass.SetActive(false);
                        PlayerItemInteraction.Click = this.gameObject;

                        if (target.gameObject.name.Contains("재료")&&!isEating)
                        {
                            ItemInfo go = new ItemInfo("Tteokbokki", img, 40, 40);
                            if (PlayerItemInteraction.Item.Count == 0)
                                PlayerItemInteraction.Item.Add(KeyCode.Alpha1, go);
                            else if (PlayerItemInteraction.Item.Count == 1)
                                PlayerItemInteraction.Item.Add(KeyCode.Alpha2, go);
                            else if (PlayerItemInteraction.Item.Count == 2)
                                PlayerItemInteraction.Item.Add(KeyCode.Alpha3, go);
                            else if (PlayerItemInteraction.Item.Count == 3)
                                PlayerItemInteraction.Item.Add(KeyCode.Alpha4, go);
                            else if (PlayerItemInteraction.Item.Count == 4)
                                PlayerItemInteraction.Item.Add(KeyCode.Alpha5, go);
                            isEating = true;
                        }
                    }
                    isClick = false;
                }
            }
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
