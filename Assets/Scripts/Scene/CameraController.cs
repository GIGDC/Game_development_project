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
        } // �ߺ��� MainCamera ������Ʈ�� ���� ��� ������Ʈ �ı�
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-30f); //�÷��̾�� z���� ������ �Կ� �Ұ���
    }
}
