using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainableByClick : MonoBehaviour
{
    GameObject player;
    public Image EventImage;
    public Sprite Image;
    GameObject target; // ���콺�� ����Ű�� ���
    public GameObject glass;
    bool isCollider = false;
    string sceneRelatedToThisKey; // ������Ʈ�� key�� �� � ���� ���õ� key����
    [Tooltip("������ ȹ�� �� �ʿ��� �������� ���̰� �� ������(default: false)")]
    public bool activeAfterObtaining = false;
    /*
    activeAfterObtaining�� 
    [Tooltip("���� �����ϰ� �ִ� �̼� ��ȣ")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("�̼� ���� ��Ȳ�� �� %�� �� �̺�Ʈ�� active�� ������ ����")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("�۾� �Ϸ� �� �̼� ���� ��Ȳ ����")]
    [SerializeField] float missionProgress;
    ���� ���� �����ϱ�
     */
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
            if(Input.GetKey(KeyCode.Escape)) // esc�� ��Ŭ�� �� �� ����
            {
                this.gameObject.SetActive(activeAfterObtaining); // ȹ�� �� �ش� ������Ʈ�� ��� Ȱ��ȭ�Ǿ� ���� ������

                if (EventImage.gameObject.name.Contains("GymStorage")&&Image!=null)
                {
                    ItemInfo go = new ItemInfo("StorageKey", Image, 40, 20);
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
                }
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

                    
                    FemaleConversation.isSuccess = true;
                }
                
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
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Item"));

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            isCollider = true;
        }
    }
}
