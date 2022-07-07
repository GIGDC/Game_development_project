using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigeGhostController : MonoBehaviour
{
    Rigidbody2D rigid;
    GameObject trackTarget;
    bool trackControl = false;
    public AudioSource audio;
    public AudioClip Sliding;

    private void Start()
    {
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trackControl = false;
            audio.loop = false;
            audio.Stop();
        }
    }
    private void OnTriggerStay2D(Collider2D collision) //추격자의 zone영역의 접촉면에 닿으면 true
    {
        if (collision.CompareTag("Player"))
        {
            audio.clip = Sliding;
            audio.loop = true;
            audio.Play();
            trackControl = true;
            trackTarget = collision.gameObject;
        }
    }

    private void Update()
    {
        Debug.Log("현재 플레이어가 근처에 있는가" + trackControl);
        if (trackControl)
        {
            if (trackTarget != null)
            {
                float Xdir = trackTarget.transform.position.x - transform.position.x;
                float Ydir = trackTarget.transform.position.y - transform.position.y;
                Xdir = (Xdir < 0) ? -1 : 1;
                Ydir = (Ydir < 0) ? -1 : 1;
                this.transform.Translate(new Vector2(Xdir, Ydir) * 5f * Time.deltaTime);
            }
        }
    }
        
}
