using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyController : MonoBehaviour
{
    public int SceneNum;
    DoorTransfer door;
    AudioSource Openaudio;
    void Start()
    {
        LockController.isLock = false;
        door = GameObject.FindObjectOfType<DoorTransfer>();
        Openaudio = door.GetComponent<AudioSource>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("¿­¼è¾ø¾îÁü");
            door.GetComponent<Animator>().SetBool("isOpening",true);
            Openaudio.Play();
            LockController.isLock = true;
            this.gameObject.SetActive(false);
        }
    }
}
