using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CupGameController : MonoBehaviour
{

    int eyeNum = -1;
    Image[] Objects;
    public Image eye;
    int[] CupLoc;
    bool isCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        CupLoc = new int[3];
        eyeNum = Random.Range(1, 3); //1���� 3����.. ������ ��ġ�� ��� �̵��ϸ�ȴ�.
        Objects = this.gameObject.GetComponentsInChildren<Image>();

        foreach(Image cup in Objects){
            if (cup.name.Contains(eyeNum.ToString()))
            {
                Debug.Log("������ ������ġ: "+eyeNum.ToString());
                eye.transform.position= new Vector3(cup.transform.position.x, eye.transform.position.y,0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCheck)
        {
            CreateUnDuplicateRandom(1, 3);
            /*
            for (int i = 0; i < 3; i++)
            {
                while (Objects[i].transform.position.x != Objects[CupLoc[i]].transform.position.x)
                {
                    if (Objects[CupLoc[i]].transform.position.x - Objects[i].transform.position.x >= 0) //����϶�
                    {
                        Objects[i].transform.position = new Vector2(Objects[i].transform.position.x + 2f, Objects[0].transform.position.y);
                    }
                    else
                    {
                        Objects[i].transform.position = new Vector2(Objects[i].transform.position.x - 2f, Objects[0].transform.position.y);
                    }
                }
            }*/
            isCheck = true;
        }
           // StartCoroutine(SwitchLoc());
    }

    public IEnumerator SwitchLoc()
    {
        CreateUnDuplicateRandom(1, 3);

        for (int i = 0; i <3; i++)
        {
            while (Objects[i].transform.position.x != Objects[CupLoc[i]].transform.position.x)
            {
                if (Objects[CupLoc[i]].transform.position.x - Objects[i].transform.position.x >= 0) //����϶�
                {
                    Objects[i].transform.position = new Vector2(Objects[i].transform.position.x + 2f, Objects[0].transform.position.y);
                }
                else
                {
                    Objects[i].transform.position = new Vector2(Objects[i].transform.position.x - 2f, Objects[0].transform.position.y);
                }
            }

           // if (Objects[i].transform.position.x == Objects[CupLoc[i]].transform.position.x)
            //    isCheck = true;
        }
        return null;
    }
    void CreateUnDuplicateRandom(int min, int max)
    {
        int currentNumber = Random.Range(min, max);
        List<int> cubeList = new List<int>();
        for (int i = 0; i < max;)
        {
            if (cubeList.Contains(currentNumber))
            {
                currentNumber = Random.Range(min, max);
            }
            else
            {
                cubeList.Add(currentNumber);
                i++;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            CupLoc[i] = cubeList[i];

            Debug.Log("�� ��ġ ����" + CupLoc[i].ToString());
        }
    }
}
