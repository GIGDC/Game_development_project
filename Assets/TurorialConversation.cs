using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurorialConversation : ActiveConversation
{
    private void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log("npc ¸¸³² 22");
        if (collision.gameObject.name != "Player")
            return;

        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                message.SetActive(true);
                clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                isChating = true;
                StartCoroutine(TextPractice());
            }

    }
}
