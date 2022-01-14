using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private int numOfAmulets ;
    public float rangeOfItemUse;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        numOfAmulets = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            UseItem();
    }
    
    public void UseItem()
    {
        if (numOfAmulets == 0)
            return;

        numOfAmulets--;
        Debug.Log("남은 부적 개수: " + numOfAmulets);

        float distanceToClosestMonster = Mathf.Infinity;
        Monster cloestMonster = null;
        Monster[] monsters = GameObject.FindObjectsOfType<Monster>();

        foreach(Monster currentMonster in monsters)
        {
            float distanceToCurrentMonster = (currentMonster.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToCurrentMonster < distanceToClosestMonster)
            {
                distanceToClosestMonster = distanceToCurrentMonster;
                cloestMonster = currentMonster;
            }
        }

        if (distanceToClosestMonster > rangeOfItemUse)
            return;
        
        // 부적 사용 시 크리처(closestMonster) 스턴 코드 넣기
        Debug.Log("부적 사용 성공");
    }

    public void ObtainItem()
    {
        numOfAmulets++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Amulet")
            numOfAmulets++;
    }
}
