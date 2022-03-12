using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    PlayerMovement player;
    static public int MapNum;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();

        if (startPoint == PlayerMovement.CurrentMapName)
        {

            if (MapNum == 1)
            {
                player.transform.position = new Vector2(-38.91f, -33.13f);
            }
            else if (MapNum == 4)
            {

                player.transform.position = new Vector2(50f, 10.2f);
            }
            else
            {
                player.transform.position = transform.position;
            }
        }

    }
}
