using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainableByClick : MonoBehaviour
{
    GameObject player;
    public Image EventImage;
    GameObject target; // ���콺�� ����Ű�� ���
    public GameObject glass;
    bool isCollider = false;
    string sceneRelatedToThisKey; // ������Ʈ�� key�� �� � ���� ���õ� key����
    [Tooltip("������ ȹ�� �� �ʿ��� �������� ���̰� �� ������(default: false)")]
    public bool activeAfterObtaining = false;
    [Tooltip("�̼� ���� ��Ȳ ����")]
    [SerializeField] float missionProgress;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
        string gameObjectName = this.gameObject.CompareTag("Key") ? this.gameObject.name + " ����" : this.gameObject.name;
        if (player.GetComponent<PlayerMissionItem>().GetMissionItem(gameObjectName) != null)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(EventImage.gameObject.activeSelf)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                this.gameObject.SetActive(activeAfterObtaining);
                EventImage.gameObject.SetActive(false);
                player.GetComponent<PlayerMovement>().CanPlayerMove = true;
                GameObject.FindObjectOfType<GameManager>().Mission1Progress += missionProgress; // �̼� ���൵

                if (this.gameObject.CompareTag("Key")) // Ŭ���� ������Ʈ�� ������ ��
                {
                    List<string> openDoorList = GameManager.openDoorList;
                    sceneRelatedToThisKey = this.gameObject.name.Split(' ')[0];
                    if (!openDoorList.Contains(sceneRelatedToThisKey))
                    { 
                        openDoorList.Add(sceneRelatedToThisKey);
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
