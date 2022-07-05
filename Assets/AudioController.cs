using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    void Awake()
    {
        GameObject[] Audio = GameObject.FindGameObjectsWithTag("Audio");
        if (Audio.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } // �ߺ��� MainCamera ������Ʈ�� ���� ��� ������Ʈ �ı�
    }
}
