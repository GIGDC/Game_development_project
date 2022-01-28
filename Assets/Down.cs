using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            Debug.Log("ÇÏ º®");
            Monster.Down_Collision = true;
            Monster.Stop = true;
            Monster.direction = Monster.Vector3FromAngle(90, 90);
        }
    }
}
