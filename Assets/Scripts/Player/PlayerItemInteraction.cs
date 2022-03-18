using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    Animator animator;

    private int numOfAmulets ;
    [Header("근접 거리")]
    [SerializeField] [Range(0f, 50f)] float rangeOfItemUse;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        numOfAmulets = 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (numOfAmulets > 0)
            {
                animator.SetTrigger("UseAmulet");
                if (Vector3.Distance(transform.position, GameObject.Find("Monster").transform.position) < rangeOfItemUse * 10f)
                    GameObject.Find("Monster").GetComponent<MonsterStatus>().attacked = true;
                //numOfAmulets--;
            }
        }
    }
}
