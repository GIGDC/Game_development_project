using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            Monster.Left_Collision = true;
            Monster.direction = Monster.Vector3FromAngle(0, 180);
            Monster.Stop = true;
            
        }
    }

}
