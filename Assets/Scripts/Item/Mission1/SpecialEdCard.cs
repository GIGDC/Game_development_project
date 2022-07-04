using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEdCard : MonoBehaviour
{
    GameObject player;
    public Image cardImage;
    public Image knifeImage;
    GameObject target; // 마우스가 가리키는 대상
    public GameObject glass;
    [Tooltip("ActivateBoardUI 스크립트가 있는 GameObject(카드 클릭 시 옆의 칠판도 같이 클릭되어 ui가 나타나므로 방지 설정)")]
    public GameObject gameObjectWithActivateBoardUI;
    [Tooltip("ClickController 스크립트가 있는 GameObject(카드 클릭 시 옆의 칠판도 같이 클릭되어 ui가 나타나므로 방지 설정)")]
    public GameObject gameObjectWithClickController;
    bool isCollider = false;
    [Tooltip("아이템 획득 후 맵에서 아이템을 보이게 할 것인지(default: false)")]
    public bool activeAfterObtaining = false;
    bool isCardCut; // 카드가 잘라진 상태인지

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
        isCardCut = false;
        if(player.GetComponent<PlayerMissionItem>().GetMissionItem(this.gameObject.name) != null)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (cardImage.gameObject.activeSelf)
        {
            gameObjectWithActivateBoardUI.GetComponent<ActivateBoardUI>().enabled = false;
            gameObjectWithClickController.GetComponent<ClickController>().enabled = false;
            if(!isCardCut && player.GetComponent<PlayerMissionItem>().GetMissionItem("커터칼") != null) // 플레이어가 커터칼 소유 시 화면에 이미지 표시
            {
                knifeImage.gameObject.SetActive(true);
            }
            else
            {
                knifeImage.gameObject.SetActive(false);
            }

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
                    this.gameObject.SetActive(activeAfterObtaining);
                    player.GetComponent<PlayerMissionItem>().AddMissionItem(this.gameObject.name); // 획득한 미션 아이템 리스트에 추가
                }
                cardImage.gameObject.SetActive(false);
                player.GetComponent<PlayerMovement>().CanPlayerMove = true;
                gameObjectWithActivateBoardUI.GetComponent<ActivateBoardUI>().enabled = true;
                gameObjectWithClickController.GetComponent<ClickController>().enabled = true;
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
