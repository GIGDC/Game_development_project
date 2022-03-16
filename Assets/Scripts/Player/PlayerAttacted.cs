using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacted : MonoBehaviour
{
    
    static public float hp;
    static public float maxHP;

    public bool zeroHP = false;
   
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        hp = 100;
        maxHP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0)
        {
            zeroHP = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            player.animator.SetBool("BeAttacked", true);
            StartCoroutine(ClockController.ChangeAttack());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            hp = hp - (float)0.1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.animator.SetBool("BeAttacked", false); 
    }

}
