using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThreeConversation : ActiveConversation
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
                ThreeMission.gameObject.SetActive(true);
            }
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Id = id;
        if (collision.gameObject.name != "Player")
            return;

        if (ThreeMission.gameObject.activeSelf == false)
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
