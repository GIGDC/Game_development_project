using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    Animator animator;
    GameObject monster;

    private int numOfAmulets;
    [Header("근접 거리")]
    [SerializeField] [Range(0f, 50f)] float rangeOfItemUse;
    PlayerMovement player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        monster = GameObject.Find("Monster");
        numOfAmulets = 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && numOfAmulets > 0 && !player.isDoor)
        {
            if (monster != null)
            {
                player.isReady = Vector3.Distance(transform.position, monster.transform.position) < rangeOfItemUse;
                if (player.isReady)
                {
                    animator.SetTrigger("UseAmulet");
                    GameObject.Find("Monster").GetComponent<MonsterStatus>().attacked = true;
                    GameObject.Find("Monster").GetComponent<Monster>().animator.SetBool("isAttacked", true);
                    GameObject.Find("Monster").GetComponent<Monster>().animator.SetBool("isWalking", false);
                    GameObject.Find("Monster").GetComponent<Monster>().animator.SetBool("isAttacking", false);
                    //numOfAmulets--;
                }
            }
        }
        if (monster != null)
        {
            GameObject.Find("Monster").GetComponent<Monster>().animator.SetBool("isAttacked", false);
            GameObject.Find("Monster").GetComponent<Monster>().animator.SetBool("isWalking", true);
            GameObject.Find("Monster").GetComponent<Monster>().animator.SetBool("isAttacking", false);
        }
    }
}
