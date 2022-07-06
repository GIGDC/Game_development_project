using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeUIImage : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    GameObject specialEdCard;
    int totalCutLine; // 현재까지 잘라진 선
    public AudioSource audio;
    public AudioClip bgm;
    private void Start()
    {
        audio.clip = bgm;
        specialEdCard = GameObject.Find("도움반카드").gameObject;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(195, 220, 0);
        rectTransform.rotation = Quaternion.Euler(0, 0, 98);
        totalCutLine = 0;
    }

    private void Update()
    {
        if (totalCutLine == 0)
        {
            if (rectTransform.anchoredPosition.x > 195)
            {// 칼이 오른쪽으로 움직일 수 있는 범위 제한
                rectTransform.anchoredPosition = new Vector2(195, 220);
            }
            else if (rectTransform.anchoredPosition.x < 10) // 칼이 첫 번째 선을 다 자름
            {
                totalCutLine++;
                rectTransform.anchoredPosition = new Vector2(-120, 25); // 두 번째 선을 자르기 위해 칼 위치 지정
                rectTransform.rotation = Quaternion.Euler(0, 0, 185);
            }
        }
        else if (totalCutLine == 1)
        {

            if (rectTransform.anchoredPosition.x < -110)
            {
                rectTransform.anchoredPosition = new Vector2(-120, 25);
            }
            else if (rectTransform.anchoredPosition.x > 180)
            {
                totalCutLine++;
                rectTransform.anchoredPosition = new Vector2(270, -55); // 세 번째 선을 자르기 위해 칼 위치 지정
                rectTransform.rotation = Quaternion.Euler(0, 0, 140);
            }
        }
        else
        {
            if (rectTransform.anchoredPosition.x > 270)
            {   
                rectTransform.anchoredPosition = new Vector2(270, -55);
            }
            else if (rectTransform.anchoredPosition.x < 170) // 세 번째 선까지 다 자름
            {
                specialEdCard.GetComponent<SpecialEdCardAndKnife>().IsCardCut = true;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        audio.Play();
        // 이전 이동과 비교해서 얼마나 이동했는지를 보여줌
        // 캔버스의 스케일과 맞춰야 하기 때문에
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if (totalCutLine == 0)
            rectTransform.anchoredPosition = new Vector2((rectTransform.anchoredPosition.y + 72.5f) * (2 / 3f), rectTransform.anchoredPosition.y);
        else if (totalCutLine == 1)
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, (-4 / 15f) * rectTransform.anchoredPosition.x + 7);
        else
            rectTransform.anchoredPosition = new Vector2((rectTransform.anchoredPosition.y - 849.5f) / (-3.35f), rectTransform.anchoredPosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
