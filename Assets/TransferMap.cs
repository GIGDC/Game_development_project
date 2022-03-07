using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransferMap : GameManager
{
    // GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        // manager = GameObject.FindObjectOfType<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return; //여기에 objcet 숨겨진 후 몇초 뒤에 등장하도록 구현 (ex: 몹)
        transferScene = "FirstAnnex";
        //manager.transferScene = "FirstAnnex";
        SceneTransition();

    }
    public void SceneTransition()
    {
        StartCoroutine(AsyncLoadMap());
    }

    override protected IEnumerator AsyncLoadMap()
    {
        //player.CurrentMapName = transferScene;
        AsyncOperation async = SceneManager.LoadSceneAsync(transferScene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {

            Debug.Log("비동기화 진행도: " + async.progress);

            if (async.progress >= 0.9f)
            {
                //StartCoroutine(FadeIn());
                async.allowSceneActivation = true;

            }
            yield return null;
        }
    }
}