using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyActiveAfterMissionDone : MonoBehaviour
{
    [Tooltip("����/�Ҹ���Ѿ� �ϴ� ������ inactive������ key ��Ÿ��")]
    public GameObject ghost;
    [SerializeField] GameObject key;
    [Tooltip("�ش� ���谡 ��� ���� �����Ǿ��ִ���")]
    [SerializeField] string sceneName;

    // Update is called once per frame
    void Update()
    {
        if(ghost.activeSelf || GameManager.openDoorList.Contains(sceneName))
            key.SetActive(false);
        else 
            key.SetActive(true);
    }
}
