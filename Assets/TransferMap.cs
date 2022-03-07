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
            return; //���⿡ objcet ������ �� ���� �ڿ� �����ϵ��� ���� (ex: ��)
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

            Debug.Log("�񵿱�ȭ ���൵: " + async.progress);

            if (async.progress >= 0.9f)
            {
                //StartCoroutine(FadeIn());
                async.allowSceneActivation = true;

            }
            yield return null;
        }
    }
}