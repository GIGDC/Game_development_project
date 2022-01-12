using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public int moveSpeed;
    Vector2 movement;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;

        animator.SetInteger("WalkHorizontally", (int)movement.x);
        animator.SetInteger("WalkVertically", (int)movement.y);
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + movement * moveSpeed * Time.deltaTime);
    }
}
