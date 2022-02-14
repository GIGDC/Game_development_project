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
        Debug.Log("호출");
        OutDoor = true;
        monster = Instantiate(prefabs,new Vector3(-9f,8f,0), Quaternion.identity);
        Vector3 tmp = new Vector3(-11f, 7f, 0); //이동할 임시 위치
        Vector3 v = tmp - (Vector3)monster.transform.position;
        Monster.direction = Vector3FromAngle(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg); //이동후 원래대로 동작!
        Debug.Log(Monster.direction);

        CancelInvoke("InvokeTest");
    }
    //같은 함수는 하나의 클래스에 묶어서 관리하도록 정리할 예정.
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
