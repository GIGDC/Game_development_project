using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerMovement player;

    void Awake()
    {
        GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");
        if (mainCameras.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } // 중복된 MainCamera 오브젝트가 있을 경우 오브젝트 파괴
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-30f); //플레이어와 z축이 같으면 촬영 불가능
    }
}
