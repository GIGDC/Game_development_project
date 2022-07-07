using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            animator.SetBool("isMoving", true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("isMoving", false);
    }
}
