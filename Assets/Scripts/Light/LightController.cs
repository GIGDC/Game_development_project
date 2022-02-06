using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        temp.y = 0.8F;
        temp.z = 0.0F;
        this.transform.position = thePlayer.transform.position - temp;

        vector.Set(thePlayer.direction.x, thePlayer.direction.y);
        if (vector.x == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            this.transform.rotation = rotation;
            temp.x = -1.0F;
            temp.y = -0.5F;
            temp.z = 0.0F;
            this.transform.position = thePlayer.transform.position - temp;
        }
        else if (vector.x == -1f)
        {
            rotation = Quaternion.Euler(0, 0, -90);
            this.transform.rotation = rotation;
            temp.x = 1.0F;
            temp.y = -0.5F;
            temp.z = 0.0F;
            this.transform.position = thePlayer.transform.position - temp;
        }
        else if (vector.y == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            this.transform.rotation = rotation;
            temp.x = 0.0F;
            temp.y = -1.0F;
            temp.z = 0.0F;
            this.transform.position = thePlayer.transform.position - temp;
        }
        else if (vector.y == -1f)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = rotation;
        }
    }
}
