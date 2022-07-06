using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public string Name; //������ �̸�
    public float PlusHp; //Hp�� �÷��ִ� item
    public float PlusMin; //���� �÷��ִ� item
    public Sprite Img;
    public int Width;
    public int Height;
    public int Inven_Width;
    public int Inven_Height;

    public ItemInfo(string name,Sprite img,int width,int height)
    {
        Name = name;
        Width = width;
        height = height;
        Img = img;
    }
}
