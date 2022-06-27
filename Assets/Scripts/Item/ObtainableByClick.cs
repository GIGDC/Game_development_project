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
    string sceneRelatedToThisKey; // ������Ʈ�� key�� �� � ���� ���õ� key����

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

                if (this.gameObject.CompareTag("Key")) // Ŭ���� ������Ʈ�� ������ ��
                {
                    List<string> openDoorList = GameManager.openDoorList;
                    sceneRelatedToThisKey = this.gameObject.name.Split(' ')[0];
                    if (!openDoorList.Contains(sceneRelatedToThisKey))
                    { openDoorList.Add(sceneRelatedToThisKey);
                        Debug.Log("�׽�Ʈ "  + sceneRelatedToThisKey);
                    } // �رݵ� �� ��Ͽ� �ش� ���谡 �� �� �ִ� �� �߰�
                }
                else
                    player.GetComponent<PlayerMissionItem>().AddMissionItem(this.gameObject.name); // ȹ���� �̼� ������ ����Ʈ�� �߰�
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
