using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainableByClick : MonoBehaviour
{
    GameObject player;
    public Image EventImage;
    public Sprite Image;
    GameObject target; // 마우스가 가리키는 대상
    public GameObject glass;
    bool isCollider = false;
    string sceneRelatedToThisKey; // 오브젝트가 key일 때 어떤 씬과 관련된 key인지
    [Tooltip("아이템 획득 후 맵에서 아이템을 보이게 할 것인지(default: false)")]
    public bool activeAfterObtaining = false;
    /*
    activeAfterObtaining을 
    [Tooltip("현재 진행하고 있는 미션 번호")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("미션 진행 상황이 몇 %일 때 이벤트를 active할 것인지 지정")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("작업 완료 시 미션 진행 상황 설정")]
    [SerializeField] float missionProgress;
    으로 추후 변경하기
     */
    [Tooltip("미션 진행 상황 설정")]
    [SerializeField] float missionProgress;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
        string gameObjectName = this.gameObject.CompareTag("Key") ? this.gameObject.name + " 열쇠" : this.gameObject.name;
        if (player.GetComponent<PlayerMissionItem>().GetMissionItem(gameObjectName) != null)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(EventImage.gameObject.activeSelf)
        {
            if(Input.GetKey(KeyCode.Escape)) // esc나 좌클릭 시 문 열림
            {
                this.gameObject.SetActive(activeAfterObtaining); // 획득 후 해당 오브젝트가 계속 활성화되어 있을 것인지

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
                GameObject.FindObjectOfType<GameManager>().Mission1Progress += missionProgress; // 미션 진행도

                if (this.gameObject.CompareTag("Key")) // 클릭한 오브젝트가 열쇠일 때
                {
                    List<string> openDoorList = GameManager.openDoorList;
                    sceneRelatedToThisKey = this.gameObject.name.Split(' ')[0];
                    if (!openDoorList.Contains(sceneRelatedToThisKey))
                    { 
                        openDoorList.Add(sceneRelatedToThisKey);
                    } // 해금된 씬 목록에 해당 열쇠가 열 수 있는 씬 추가

                    
                    FemaleConversation.isSuccess = true;
                }
                
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
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Item"));

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            isCollider = true;
        }
    }
}
