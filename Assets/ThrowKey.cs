using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKey : MonoBehaviour
{
    Rigidbody2D key;
    ActiveConversation tf;
    SpriteRenderer sr;
    private void Start()
    {
        key = GetComponent<Rigidbody2D>();
        tf =GameObject.FindObjectOfType<ActiveConversation>(); 
        sr = this.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!tf.ThrowKey)
        {
            sr.material.color = Color.clear;
        }
        else{
            key.AddForce(new Vector2(5, 3), ForceMode2D.Impulse);
            //tf.ThrowKey = false;
        }
        
    }
}
