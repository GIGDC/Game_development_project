using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private int numOfAmulets ;
    [Header("근접 거리")]
    [SerializeField] [Range(0f, 5f)] float rangeOfItemUse;

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

        Collider2D attackedMonster = Physics2D.OverlapCircle(transform.position, rangeOfItemUse, 1 << LayerMask.NameToLayer("Monster"));

        if (attackedMonster != null && attackedMonster.GetComponent<MonsterStatus>().Attacked == false)
        {
            attackedMonster.GetComponent<MonsterStatus>().Attacked = true;
            Debug.Log("부적 사용 성공");
        }
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
