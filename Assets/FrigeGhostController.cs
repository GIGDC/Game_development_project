using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigeGhostController : MonoBehaviour
{
    Rigidbody2D rigid;
    GameObject trackTarget;
    bool trackControl = false;
    private void Start()
    {
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        trackControl = false;
    }
    private void OnTriggerStay2D(Collider2D collision) //�߰����� zone������ ���˸鿡 ������ true
    {
            trackControl = true;
            trackTarget = collision.gameObject;
    }

    private void Update()
    {
        Debug.Log("���� �÷��̾ ��ó�� �ִ°�" + trackControl);
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
