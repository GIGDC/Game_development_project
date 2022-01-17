using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Item");
        Destroy(gameObject);

        GameObject player = GameObject.Find("³Ê±¸¸®") as GameObject;
        player.GetComponent<PlayerItemInteraction>().ObtainItem();
    }

    private void OnMouseDrag()
    {
        
    }
}
