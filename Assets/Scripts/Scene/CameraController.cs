using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");
    }
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
