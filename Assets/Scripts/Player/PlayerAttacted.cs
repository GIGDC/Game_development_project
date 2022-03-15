using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacted : MonoBehaviour
{
    
    static public int hp;
    static public int maxHP;

    public Sprite attack_img;

    public bool zeroHP = false;
    static int Attackedcount = 0; //당할때마다 모션 1번만 행동
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
           // player.animator.SetBool("BeAttacked", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            hp = hp - 10;

            print(hp);
            Attackedcount = 0;
            if (Attackedcount == 0)
            {
                player.animator.SetBool("BeAttacked", true);
            }
           // player.animator.SetBool("BeAttacked", false);
                
            StartCoroutine(ClockController.ChangeAttack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.animator.SetBool("BeAttacked", false);
        Attackedcount++;
    }

    

}
