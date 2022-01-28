using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall")&& !collision.gameObject.tag.Equals("Door"))
        {
            Debug.Log("À­ º®");
            Monster.Up_Collision = true;
            Monster.direction = Monster.Vector3FromAngle(90, 270);
            Monster.Stop = true;
        }
    }

}
