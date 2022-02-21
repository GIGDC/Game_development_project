using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public PlayerMovement player;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); // memory leak
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-15f); //플레이어와 z축이 같으면 촬영 불가능
    }
}
