using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Timer_60 clock;
    private Monster monster;
    private PlayerMovement player;

    void Start()
    { 
        DontDestroyOnLoad(this.gameObject); // memory leak
        clock = GameObject.FindObjectOfType<Timer_60>(); // Timer_60에 대한 clock을 찾음
        monster = GameObject.FindObjectOfType<Monster>();
        player = GameObject.FindObjectOfType<PlayerMovement>();

    }

    public void Update()
    {   
        if (clock.isStop)
        {
            monster.Hide();
            player.Hide();

            StartCoroutine(LoadMap("GameOver"));
        }
    }

    public IEnumerator LoadMap(string transferMapName)
    {
        clock.isStop = false;
        yield return new WaitForSeconds(0f);

        SceneManager.LoadScene(transferMapName);
    }

}
