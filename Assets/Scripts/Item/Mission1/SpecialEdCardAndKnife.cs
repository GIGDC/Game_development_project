using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEdCardAndKnife : MonoBehaviour
{
    GameObject player;
    [SerializeField] Image cardImage;
    [SerializeField] Image knifeImage;
    GameObject target; // 마우스가 가리키는 대상
    [SerializeField] GameObject glass;
    bool isCollider = false;
    [Tooltip("커터칼 gameObject 지정")]
    [SerializeField] GameObject knife;
    [Tooltip("PlayerMissionItem 스크립트 내 missionItem에 저장할 이름 지정")]
    [SerializeField] string missionItemName;
    bool isCardCut; // 카드가 잘라진 상태인지

    [Tooltip("현재 진행하고 있는 미션 번호")]
    [Range(1, 3)] public int mission = 1;
    [Tooltip("미션 진행 상황이 몇 %일 때 이벤트를 active할 것인지 지정")]
    [SerializeField] float activeByMissionProgress;
    [Tooltip("작업 완료 시 미션 진행 상황 설정")]
    [SerializeField] float missionProgress;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
        isCardCut = false;
        if(player.GetComponent<PlayerMissionItem>().GetMissionItem("mission1_card") == null)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.FindObjectOfType<GameManager>().Mission1Progress != activeByMissionProgress)
            this.gameObject.SetActive(false);

        if (cardImage.gameObject.activeSelf)
        {
            if (isCardCut) // 카드가 잘라지면 잘라진 카드 이미지 표시
            {
                cardImage.GetComponent<Animator>().SetTrigger("isCut");
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                if(!isCardCut) // 카드가 잘라지지 않은 상태면 플레이어가 도움반 카드 획득 불가
                {
                    this.gameObject.SetActive(true);
                }
                else
                {
                    this.gameObject.SetActive(false);
                    knife.SetActive(false);
                    GameObject.FindObjectOfType<GameManager>().Mission1Progress = missionProgress;
                    player.GetComponent<PlayerMissionItem>().AddMissionItem(missionItemName); // 획득한 미션 아이템 리스트에 추가
                }
                cardImage.gameObject.SetActive(false);
                knifeImage.gameObject.SetActive(false);
                player.GetComponent<PlayerMovement>().CanPlayerMove = true;
            }
        }
    }

    private void FixedUpdate()
    {
        CastRay();
        if (isCollider && target.CompareTag("magnifiedObj"))
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
                cardImage.gameObject.SetActive(true);
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

    public bool IsCardCut
    {
        get { return isCardCut; }
        set { isCardCut = value; }
    }
}
