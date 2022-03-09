using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            Debug.Log("ÀâÇû´Ù");
           // animator.SetBool("isWalking", false);
           // directionChangeInterval = 5f;
        }
    }
}
