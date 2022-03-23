using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraShake : MonoBehaviour
{

    public Camera mainCamera;
    Vector3 cameraPos;
    DoorTransfer player;
    public Image WarningUI;
    [SerializeField] [Range(0.01f, 0.5f)] float shakeRange = 0.5f;
    [SerializeField] [Range(0.1f, 1f)] float duration = 1f;


    public void Shake()
    {
        player = GameObject.FindObjectOfType<DoorTransfer>();
        GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");
        if (mainCameras.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } // 중복된 MainCamera 오브젝트가 있을 경우 오브젝트 파괴

        cameraPos = mainCameras[0].transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }

    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    void StopShake()
    {
        player.isOpeningDoor = false;
        WarningUI.gameObject.SetActive(false);
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraPos;
    }
}
