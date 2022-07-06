using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerItemInteraction : MonoBehaviour
{
    Animator animator;
    GameObject monster;
    private int numOfAmulets ; //�������� Dictionary�� �����ؼ�
    [Header("���� �Ÿ�")]
    [SerializeField] [Range(0f, 50f)] float rangeOfItemUse;
    DoorTransfer player;

    static public GameObject Inventory; 
    static public Dictionary<KeyCode, ItemInfo> Item; //Player�� �������ִ� item����, ������ �̰� ���忹�� ������ ���������� ���忹��.
    static public int Count; //�����̸� ���濹�� 1~5���� ���� ���� keyCode��������.
    static public GameObject Click; //�����̸� ��������
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
    
    void EatingGameObject(GameObject go)
    {
        if (Item.Count == 0)
            Item.Add(KeyCode.Alpha1, go.gameObject.GetComponent<ItemInfo>());
        else if (Item.Count == 1)
            Item.Add(KeyCode.Alpha2, go.gameObject.GetComponent<ItemInfo>());
        else if (Item.Count == 2)
            Item.Add(KeyCode.Alpha3, go.gameObject.GetComponent<ItemInfo>());
        else if (Item.Count == 3)
            Item.Add(KeyCode.Alpha4, go.gameObject.GetComponent<ItemInfo>());
        else if (Item.Count == 4)
            Item.Add(KeyCode.Alpha5, go.gameObject.GetComponent<ItemInfo>());
        
        go.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            EatingGameObject(collision.gameObject);
        }
        
    }
    void Update()
    {
        if(Click!=null)
        {
            EatingGameObject(Click.gameObject);
            Click = null;
        }

        if (Item != null)
        {
            foreach (KeyCode key in Item.Keys)
            {
                Debug.Log(Item[key].Name);
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

                if (Item[key].Name == "Eyes" && Input.GetKeyDown(key))
                {
                    if (PlayerAttacted.hp < 100)
                    {
                        PlayerAttacted.hp = 100;
                        Item.Remove(key);
                        Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                        break;
                    }
                }
                if (Item[key].Name == "Drinking" && Input.GetKeyDown(key))
                {
                    if (PlayerAttacted.hp < 100)
                    {
                        PlayerAttacted.hp += Item[key].PlusHp;
                        Item.Remove(key);
                        Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                        break;
                    }
                }
                if (Item[key].Name == "Chip" && Input.GetKeyDown(key))
                {
                    if (PlayerAttacted.hp < 100)
                    {
                        PlayerAttacted.hp += Item[key].PlusHp;
                        Item.Remove(key);
                        Inventory.transform.GetChild((int)key - 49).GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                        break;
                    }
                }
            }
        }
    }
}
