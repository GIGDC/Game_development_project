using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainableByClick : MonoBehaviour
{
    GameObject player;
    public Image EventImage;
    GameObject target;
    public GameObject glass;
    bool isCollider = false;
    string sceneRelatedToThisKey; // 오브젝트가 key일 때 어떤 씬과 관련된 key인지

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    private void Update()
    {
        if(EventImage.gameObject.activeSelf)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                this.gameObject.SetActive(false);
                EventImage.gameObject.SetActive(false);
                player.GetComponent<PlayerMovement>().CanPlayerMove = true;

                if (this.gameObject.CompareTag("Key")) // 클릭한 오브젝트가 열쇠일 때
                {
                    List<string> openDoorList = GameManager.openDoorList;
                    sceneRelatedToThisKey = this.gameObject.name.Split(' ')[0];
                    if (!openDoorList.Contains(sceneRelatedToThisKey))
                    { openDoorList.Add(sceneRelatedToThisKey);
                        Debug.Log("테스트 "  + sceneRelatedToThisKey);
                    } // 해금된 씬 목록에 해당 열쇠가 열 수 있는 씬 추가
                }
                else
                    player.GetComponent<PlayerMissionItem>().AddMissionItem(this.gameObject.name); // 획득한 미션 아이템 리스트에 추가
            }
        }
    }

    private void FixedUpdate()
    {
        CastRay();
        if (isCollider && (target.CompareTag("magnifiedObj") || target.CompareTag("Key")))
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
                player.GetComponent<PlayerMovement>().CanPlayerMove = false;
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
