using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpingController2 : MonoBehaviour
{
    public Image img;
    public AudioSource audio;
    public AudioClip As;
    private void Start()
    {
        audio.clip = As;
    }
    IEnumerator SizeCoroutine(float time)
    {
        float elapsedTime = 0.0f;
        int count = 0;
        while (elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);
            count++;
            img.rectTransform.sizeDelta
                = new Vector2(350 + (20 * count), 380 + (20 * count));

            yield return null;
        }
        img.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audio.Play();
        img.gameObject.SetActive(true);
        StartCoroutine(SizeCoroutine(1f));
    }
}
