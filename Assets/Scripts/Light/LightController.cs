using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    private PlayerMovement thePlayer;  // 플레이어가 바라보고 있는 방향
    private Vector2 vector;
    public GameObject on;

    private Quaternion rotation;  // 회전(각도)을 담당하는 Vector4

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp;
        temp.x = 0.0F;
        temp.y = -0.5F;
        temp.z = 0.0F;
        this.transform.position = thePlayer.transform.position - temp;

        vector.Set(thePlayer.direction.x, thePlayer.direction.y);
        if (vector.x == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            this.transform.rotation = rotation;
            temp.x = -0.4F;
            temp.y = -0.3F;
            temp.z = 0.0F;
            this.transform.position = thePlayer.transform.position - temp;
        }
        else if (vector.x == -1f)
        {
            rotation = Quaternion.Euler(0, 0, -90);
            this.transform.rotation = rotation;
            temp.x = 0.3F;
            temp.y = -0.5F;
            temp.z = 0.0F;
            this.transform.position = thePlayer.transform.position - temp;
        }
        else if (vector.y == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            this.transform.rotation = rotation;
            temp.x = 0.0F;
            temp.y = 0.0F;
            temp.z = 0.0F;
            this.transform.position = thePlayer.transform.position - temp;
        }
        else if (vector.y == -1f)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = rotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.tag == "Monster")
        //{
        //    Color color = collision.GetComponent<Image>.color;
        //    color.a = 255f;
        //    collision.GetComponent<Image>().color = color;
        //    print("몬스터 등장");
        //}
    }
}
