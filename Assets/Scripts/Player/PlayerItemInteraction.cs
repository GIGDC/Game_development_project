using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerItemInteraction : MonoBehaviour
{
    Animator animator;
    GameObject monster;
    private int numOfAmulets ; //삭제예정 Dictionary로 보관해서
    [Header("근접 거리")]
    [SerializeField] [Range(0f, 50f)] float rangeOfItemUse;
    DoorTransfer player;

    static public GameObject Inventory; 
    static public Dictionary<KeyCode, ItemInfo> Item; //Player가 가지고있는 item으로, 앞으로 이곧에 저장예정 게임이 종료전까지 저장예정.
    static public int Count; //변수이름 변경예정 1~5까지 수를 세서 keyCode설정예정.
    private void Start()
    {
        numOfAmulets = 0;
        Count = -1; 
        Item = new Dictionary<KeyCode, ItemInfo>();
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player= FindObjectOfType<DoorTransfer>();
        monster = GameObject.Find("Monster");
        Inventory=GameObject.Find("Inventory");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Amulet")|| collision.CompareTag("Hourglass")|| collision.CompareTag("Lunch"))
        {
            if (Item.Count == 0)
                Item.Add(KeyCode.Alpha1, collision.gameObject.GetComponent<ItemInfo>());
            else if(Item.Count == 1)
                Item.Add(KeyCode.Alpha2, collision.gameObject.GetComponent<ItemInfo>());
            else if (Item.Count == 2)
                Item.Add(KeyCode.Alpha3, collision.gameObject.GetComponent<ItemInfo>());
            else if (Item.Count == 3)
                Item.Add(KeyCode.Alpha4, collision.gameObject.GetComponent<ItemInfo>());
            else if (Item.Count == 4)
                Item.Add(KeyCode.Alpha5, collision.gameObject.GetComponent<ItemInfo>());
            else //5이상일시
            {
                Count++;
                Item.Add(KeyCode.LeftControl, collision.gameObject.GetComponent<ItemInfo>());
            }
            collision.gameObject.SetActive(false);
        }
        
    }
    void Update()
    {
        if (Item != null)
        {
            foreach (KeyCode key in Item.Keys)
            {
                if (Item[key].Name == "Amulet" && Input.GetKeyDown(key))
                {
                    
                    animator.SetTrigger("UseAmulet");
                    Item.Remove(key);
                    Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                    break;
                    if (monster != null)
                    {
                        if (Vector3.Distance(transform.position, monster.transform.position) < rangeOfItemUse)
                        {
                            animator.SetTrigger("UseAmulet");
                            GameObject.Find("Monster").GetComponent<MonsterStatus>().attacked = true;
                            //numOfAmulets--;
                        }
                    }
                }
                if (Item[key].Name == "Hourglass" && Input.GetKeyDown(key))
                {
                    if (Timer_60.timer > 40f)
                    {
                        Timer_60.timer -= Item[key].PlusMin;
                        Item.Remove(key);
                        Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                        break;
                    }
                }
                if (Item[key].Name == "LunchBox" && Input.GetKeyDown(key))
                {
                    if (PlayerAttacted.hp<100)
                    {
                        PlayerAttacted.hp=(PlayerAttacted.hp+Item[key].PlusHp)>100? 100: PlayerAttacted.hp + Item[key].PlusHp;
                        Item.Remove(key);
                        Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                        break;
                    }
                }
            }
        }
    }
}
