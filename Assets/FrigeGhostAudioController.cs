using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigeGhostAudioController : MonoBehaviour
{
    public GameObject bgm;
    public AudioClip GhostBgm;
    public static bool isPlaying = false;


    private void Update()
    {   if (isPlaying)
        {
            bgm.gameObject.GetComponent<AudioSource>().clip = GhostBgm;
            bgm.gameObject.GetComponent<AudioSource>().Play();
            isPlaying = false;
        }

    }
}
