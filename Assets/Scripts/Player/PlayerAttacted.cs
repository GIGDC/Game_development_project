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

        Debug.Log(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0)
        {
            //Debug.Log(zeroHP+" ZeroHP");
            zeroHP = true;
        }
        if (Monster.isStunned)
        {
            StartCoroutine(Stunned());
        }
    }

    public IEnumerator Stunned()
    {
        int countTime = 0;

        while (countTime < 10)
        {
            if (countTime % 2 == 0)
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
            else
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        GetComponent<SpriteRenderer>().color = new Color32(255, 255,255, 255);
        Monster.isStunned = false;
        yield return null;
    }

    public IEnumerator Attacked()
    {
        player.animator.SetBool("BeAttacked", true);
        yield return new WaitForSeconds(2.0f);
        player.animator.SetBool("BeAttacked", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Monster")) && Monster.isCollide&&!player.isAttacking)
        {
            StartCoroutine(Attacked());
            StartCoroutine(ClockController.ChangeAttack());
            hp = hp - (float)10f;
            player.transform.position = new Vector3(player.transform.position.x - 2f, player.transform.position.y, 0);
        }
        if (collision.gameObject.CompareTag("Hands"))
        {
            StartCoroutine(Attacked());
            StartCoroutine(ClockController.ChangeAttack());
            hp = hp - (float)10f;
            player.transform.position = new Vector3(player.transform.position.x - 2f, player.transform.position.y, 0);
        }
    }
}
