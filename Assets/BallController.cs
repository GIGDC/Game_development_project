using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip bgm;
    private void Update()
    {
        if (CountController.SocCount >= 4&& CountController.VolCount >= 5 && CountController.DodCount >= 7&& CountController.BasCount >= 2)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        audio.clip = bgm;
        audio.Play();
            if (this.name.Contains("축구공"))
            {
                CountController.SocCount++;
            }
            if (this.name.Contains("배구공"))
            {
                CountController.VolCount++;
            }
            if (this.name.Contains("피구공"))
            {
                CountController.DodCount++;
            }
            if (this.name.Contains("농구공"))
            {
                CountController.BasCount++;
            }

        this.gameObject.SetActive(false);
    }
    
}
