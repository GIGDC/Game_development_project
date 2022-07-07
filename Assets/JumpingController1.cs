using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingController1 : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip ac;

    void Start()
    {
        audio.clip = ac;
        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAttacted.hp -= 5;
        this.gameObject.SetActive(false);
        

        GameObject.Find("Eyes").transform.Find("1").gameObject.SetActive(true);
        GameObject.Find("Eyes").transform.Find("2").gameObject.SetActive(true);
        GameObject.Find("Eyes").transform.Find("3").gameObject.SetActive(true);

        audio.Play();
        Invoke("DownHead", 2f);
    }

    void DownHead()
    {
        audio.Stop();
        GameObject.Find("Eyes").transform.Find("1").gameObject.SetActive(false);
        GameObject.Find("Eyes").transform.Find("2").gameObject.SetActive(false);
        GameObject.Find("Eyes").transform.Find("3").gameObject.SetActive(false);
    }
}
