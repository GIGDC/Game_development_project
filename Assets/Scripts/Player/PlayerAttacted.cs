using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacted : MonoBehaviour
{
    public Slider slider;
    public int hp;
    public int maxHP;

    public Sprite basic_img;
    public Sprite attack_img;
    public Sprite pain1_img;
    public Sprite pain2_img;
    public Sprite pain3_img;
    public Sprite pain4_img;

    Image thisImg;  // 현재 이미지

    public bool zeroHP = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        maxHP = 100;

        thisImg = GameObject.FindGameObjectWithTag("Clock").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)hp / maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            hp = hp - 20;

            print(hp);

            if (hp == 80)
            {
                ImageChange(pain1_img);
            }
            if (hp == 60)
            {
                ImageChange(pain2_img);
            }
            if(hp == 40)
            {
                ImageChange(pain3_img);
            }
            if (hp == 20)
            {
                ImageChange(pain4_img);
            }
            if (hp == 0)
            {
                zeroHP = true;
            }

            //ImageChange(attack_img);
        }
    }

    private void ImageChange(Sprite change_img)
    {
        thisImg.sprite = change_img;
        //yield return new WaitForSeconds(1f);
        //thisImg.sprite = basic_img;
    }
}