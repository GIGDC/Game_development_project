using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstConversation : ActiveConversation
{
    public int id;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isChating)
            {
                if (Key != null)
                    Key.SetActive(true);
                message.SetActive(false);
                clock.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                secondHand.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                isChating = false;
            }
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Id = id;
        if (collision.gameObject.name != "Player")
            return;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            message.SetActive(true);
            clock.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            secondHand.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            isChating = true;
            StartCoroutine(Talk());
        }
    }
}
