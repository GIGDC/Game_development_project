using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyController : MonoBehaviour
{
    public int SceneNum;
    static public bool isLock;
    void Start()
    {
        isLock = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("¿­¼è¾ø¾îÁü");
            isLock = true;
            this.gameObject.SetActive(false);
            //DoorTransfer.doorAnimator.SetTrigger("OpenDoor");

        }
    }
}
