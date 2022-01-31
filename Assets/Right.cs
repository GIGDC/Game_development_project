using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {

            Debug.Log("¿ì º®");
            Monster.Right_Collision = true;
            Monster.direction = Monster.Vector3FromAngle(180, 0);
            Monster.Stop = true;
        }
    }

}
