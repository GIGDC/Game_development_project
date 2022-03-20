using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    PlayerMovement player;
    static public int MapNum;
    static public string direction;
    int CurMapNum;
    // Start is called before the first frame update
    void Start()
    {
        KeyController.isLock = false;
        player = GameObject.FindObjectOfType<PlayerMovement>();

        if (startPoint == PlayerMovement.CurrentMapName)
        {

            CurMapNum = SceneManager.GetActiveScene().buildIndex;
            if (MapNum == 1)
            {
                if(CurMapNum==0) //별관 -> 1F
                    player.transform.position = new Vector2(-37f, -33.54106f);
                else if (CurMapNum == 12)
                    if (direction == "front")
                    {
                        player.transform.position = new Vector2(3.1f, -1.524785f);
                    }
                    else
                    {
                        player.transform.position = new Vector2(-35.19863f, -1.524785f);
                    }
                else if (CurMapNum == 13)
                {
                    if (direction == "front")
                    {
                        player.transform.position = new Vector2(4.7f, 1.5f);
                    }
                    else
                    {
                        player.transform.position = new Vector2(-33.69657f, -1.5f);
                    }
                }
                else if (CurMapNum == 14)
                {
                    if (direction == "front")
                    {
                        player.transform.position = new Vector2(-40.82134f, 4.1f);
                    }
                    else
                    {
                        player.transform.position = new Vector2(-88.1f, 4.1f);
                    }
                }
                else if (CurMapNum == 15)
                {
                    player.transform.position = new Vector2(-33.24031f, -1.425902f);
                }
            }
            else if (MapNum == 4)
            {
                player.transform.position = new Vector2(50f, 10.2f);
            }
            else if (MapNum == 0) // 1F에서 다른 교실로 이동할때 필요한 로직 (이전 씬이 0번(즉 1F이라면) 현재씬은 ?)
            {

                if (CurMapNum == 1) //별관
                {
                    player.transform.position = new Vector2(121.2f, 0f);
                }
                else if (CurMapNum==5) //도서관
                {

                    if (direction == "front")
                    {
                        player.transform.position = new Vector2(4.12f, -1.6f);
                    }
                    else{

                        player.transform.position = new Vector2(-34.5f, -0.6f);
                    }
                }else if (CurMapNum == 6) //방송실
                {
                    if (direction == "front")
                    {
                        player.transform.position = new Vector2(-11.1f, -1.6f);
                    }
                    else
                    {
                        player.transform.position = new Vector2(-34.82566f, -1.420698f);
                    }
                }
                else if(CurMapNum == 7) //도움반 -front /back이 없음.
                {
                    player.transform.position = new Vector2(-33.54537f, -1.444537f);
                }
                else if (CurMapNum == 8) //행정실
                {
                    if (direction == "front")
                    {
                        player.transform.position = new Vector2(5f, -1.418353f);
                    }
                    else
                    {
                        player.transform.position = new Vector2(-34.09242f, -1.420698f);
                    }
                }
                else  if(CurMapNum == 9) //교장실
                {
                    player.transform.position = new Vector2(-33.09331f, -1.561083f);
                }
                else if (CurMapNum == 10) //양호실
                {
                    if (direction == "front")
                    {
                        player.transform.position = new Vector2(4.7f, -1.1f);
                    }
                    else
                    {
                        player.transform.position = new Vector2(-33.38993f, -1.614836f);
                    }
                }
                else if (CurMapNum == 11)
                {
                    player.transform.position = new Vector2(-34.78034f, -1.447208f);
                }
            }
            else if (MapNum == 5) //도서관
            {
                if (direction == "front")
                {
                    player.transform.position = new Vector2(52.51f, -32.18f);
                }
                else
                {
                    player.transform.position = new Vector2(21.58f, -32.07f);
                }
            }
            else if (MapNum == 6) //방송실에서 1F으로 나올때
            {
                if (direction == "front")
                {
                    player.transform.position = new Vector2(80.88937f, -32.02f);
                }
                else
                {
                    player.transform.position = new Vector2(62.51f, -32.18f);
                }
            }
            else if (MapNum == 7)
            {
                player.transform.position = new Vector2(104.2f, -31.73f);
            }else if (MapNum == 8) //행정실 -> 1F
            {
                if (direction == "front")
                {
                    player.transform.position = new Vector2(31.24f, 9.11f);
                }
                else
                {
                    player.transform.position = new Vector2(13.72f, 8.80f);
                }
                
            }else if (MapNum == 9) //교장실 -> 1F
            {
                player.transform.position = new Vector2(78.9f, 9.2f);

            }
            else if (MapNum == 10) //양호실 -> 1F
            {
                if (direction == "front")
                {
                    player.transform.position = new Vector2(106.7f, 9.1f);
                }
                else
                {
                    player.transform.position = new Vector2(89.5f, 8.80f);
                }
            }else if (MapNum == 11)
            {
                player.transform.position = new Vector2(-20.37747f, 8.410156f);
            }else if (MapNum == 12)
            {
                if (direction == "front")
                {
                    player.transform.position = new Vector2(102.18f, 1.2f);
                }
                else
                {
                    player.transform.position = new Vector2(83.6f, 1.12f);
                }
            }else if (MapNum == 13)
            {
                if (direction == "front")
                {
                    player.transform.position = new Vector2(71.31f, 1.12f);
                }
                else
                {
                    player.transform.position = new Vector2(56.24f, 1.12f);
                }
            }
            else if (MapNum == 14)
            {
                if (direction == "front")
                {
                    player.transform.position = new Vector2(43f, 1.12f);
                }
                else
                {
                    player.transform.position = new Vector2(27.83f, 1.12f);
                }
            }
            else if (MapNum == 15)
            {
                player.transform.position = new Vector2(14.9f, 1.12f);
            }
        }

        Debug.Log(PlayerMovement.CurrentMapName);

    }
}
