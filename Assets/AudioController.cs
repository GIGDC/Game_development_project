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
        } // 중복된 MainCamera 오브젝트가 있을 경우 오브젝트 파괴
    }
}
