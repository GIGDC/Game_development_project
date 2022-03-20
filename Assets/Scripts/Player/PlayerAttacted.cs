using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacted : MonoBehaviour
{
    
    static public float hp;
    static public float maxHP;

    public bool zeroHP = false;
    static int Attackedcount = 0; //���Ҷ����� ��� 1���� �ൿ
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
        if (hp < 0)
        {
            Debug.Log(zeroHP+" ZeroHP");
            zeroHP = true;
        }
    }

    public IEnumerator Attacked()
    {
        player.animator.SetBool("BeAttacked", true);
        yield return new WaitForSeconds(0.5f);
        player.animator.SetBool("BeAttacked", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && !player.isAttacking)
        {
            StartCoroutine(Attacked());
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
}
