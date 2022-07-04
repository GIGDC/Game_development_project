using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FrigeGhostAudioController : MonoBehaviour
{
    public GameObject bgm;
    public AudioClip GhostBgm;
    public static bool isPlaying = false;
    public Image Ghost;

    void Animation()
    {
        Ghost.gameObject.SetActive(true);
        StartCoroutine(SizeCoroutine(2f));
    }
    IEnumerator SizeCoroutine(float time)
    {
        float elapsedTime = 0.0f;
        int count = 0;
        while (elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);
            count++;
            Ghost.rectTransform.sizeDelta
                = new Vector2(350+(30*count),380+ (30 * count));

            yield return null;
        }
        Ghost.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        yield return null;
    }
    private void Update()
    {   if (isPlaying)
        {
            bgm.gameObject.GetComponent<AudioSource>().clip = GhostBgm;
            bgm.gameObject.GetComponent<AudioSource>().Play();
            Invoke("Animation", 0.8f);
            isPlaying = false;
        }

    }
}
