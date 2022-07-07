using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DownCard : MonoBehaviour
{
    public Sprite Soccer;
    public Sprite Basketball;
    public Sprite Volleyball;
    public Sprite Dodgeball;
    ClickHint[] hint;
    Image thisImg;

    private void Start()
    {
        hint = GameObject.FindObjectsOfType<ClickHint>();
        thisImg = GetComponent<Image>();
    }
    private void FixedUpdate()
    {
        if (hint[0].ImgNum == 0)
        {
            Debug.Log(0);
            thisImg.sprite = Soccer;
        }else if (hint[1].ImgNum == 1)
        {

            Debug.Log(1);
            thisImg.sprite = Basketball;
        }
        else if (hint[2].ImgNum == 2)
        {

            Debug.Log(2);
            thisImg.sprite = Volleyball;
        }
        else if (hint[3].ImgNum == 3)
        {

            Debug.Log(3);
            thisImg.sprite = Dodgeball;
        }
      
        if (Input.GetMouseButtonDown(0))
        {

            this.gameObject.SetActive(false);

        }

    }
}

