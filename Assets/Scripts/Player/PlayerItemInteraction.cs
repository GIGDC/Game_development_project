using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    Animator animator;
    GameObject monster;
    private int numOfAmulets ;
    [Header("근접 거리")]
    [SerializeField] [Range(0f, 50f)] float rangeOfItemUse;
    DoorTransfer player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player= FindObjectOfType<DoorTransfer>();
        monster = GameObject.Find("Monster");
        numOfAmulets = 3;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && numOfAmulets > 0 && !player.isOpeningDoor)
        {
            if(monster != null)
            {
                if(Vector3.Distance(transform.position, monster.transform.position) < rangeOfItemUse)
                {
                    animator.SetTrigger("UseAmulet");
                    GameObject.Find("Monster").GetComponent<MonsterStatus>().attacked = true;
                    //numOfAmulets--;
                }
            }
        }       
    }
}
