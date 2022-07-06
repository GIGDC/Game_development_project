using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JumpingController : MonoBehaviour
{
    int rand=0;
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
        rand = Random.Range(1, 3);

        Debug.Log(rand);
        GameObject.Find("Heads").transform.Find(rand.ToString()).gameObject.SetActive(true);
        audio.Play();
        Invoke("DownHead", 3f);
    }

    void DownHead()
    {
        audio.Stop();
        GameObject.Find("Heads").transform.Find(rand.ToString()).gameObject.SetActive(false);
        
    }
}
