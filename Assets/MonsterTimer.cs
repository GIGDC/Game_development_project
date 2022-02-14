using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTimer : MonoBehaviour
{
    public GameObject prefabs;
    GameObject monster;

    static public bool OutDoor = false;

    void InvokeTest()
    {
        prefabs.SetActive(true);
        TransferMap.CheckMonster = false;
        Debug.Log("ȣ��");
        OutDoor = true;
        monster = Instantiate(prefabs,new Vector3(-9f,8f,0), Quaternion.identity);
        Vector3 tmp = new Vector3(-11f, 7f, 0); //�̵��� �ӽ� ��ġ
        Vector3 v = tmp - (Vector3)monster.transform.position;
        Monster.direction = Vector3FromAngle(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg); //�̵��� ������� ����!
        Debug.Log(Monster.direction);

        CancelInvoke("InvokeTest");
    }
    //���� �Լ��� �ϳ��� Ŭ������ ��� �����ϵ��� ������ ����.
    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (TransferMap.CheckMonster)
        {
            Invoke("InvokeTest", 10f);
        }
    }
}
