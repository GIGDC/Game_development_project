using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeUIImage : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    GameObject specialEdCard;
    int totalCutLine; // ������� �߶��� ��
    public AudioSource audio;
    public AudioClip bgm;
    private void Start()
    {
        audio.clip = bgm;
        specialEdCard = GameObject.Find("�����ī��").gameObject;
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
            {// Į�� ���������� ������ �� �ִ� ���� ����
                rectTransform.anchoredPosition = new Vector2(195, 220);
            }
            else if (rectTransform.anchoredPosition.x < 10) // Į�� ù ��° ���� �� �ڸ�
            {
                totalCutLine++;
                rectTransform.anchoredPosition = new Vector2(-120, 25); // �� ��° ���� �ڸ��� ���� Į ��ġ ����
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
                rectTransform.anchoredPosition = new Vector2(270, -55); // �� ��° ���� �ڸ��� ���� Į ��ġ ����
                rectTransform.rotation = Quaternion.Euler(0, 0, 140);
            }
        }
        else
        {
            if (rectTransform.anchoredPosition.x > 270)
            {   
                rectTransform.anchoredPosition = new Vector2(270, -55);
            }
            else if (rectTransform.anchoredPosition.x < 170) // �� ��° ������ �� �ڸ�
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
        // ���� �̵��� ���ؼ� �󸶳� �̵��ߴ����� ������
        // ĵ������ �����ϰ� ����� �ϱ� ������
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
