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
    PlayerMovement player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player= FindObjectOfType<PlayerMovement>();
        monster = GameObject.Find("Monster");
        numOfAmulets = 3;
    }

    void Update()
    {
<<<<<<< HEAD
        if (monster != null) {
            player.isReady = Vector3.Distance(transform.position, monster.transform.position) < rangeOfItemUse * 10f;

            if (player.isReady && !player.isDoor) {
                if (Input.GetKeyDown(KeyCode.Space))
=======
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (numOfAmulets > 0)
            {
                if (Vector3.Distance(transform.position, GameObject.Find("Monster").transform.position) < rangeOfItemUse)
>>>>>>> 2e74e8a66e7b8e2cceb711caed72ba6316e33558
                {
                    animator.SetTrigger("UseAmulet");
                    GameObject.Find("Monster").GetComponent<MonsterStatus>().attacked = true;
                    //numOfAmulets--;
                }
            }
        }
    }
}
