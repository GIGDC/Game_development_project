using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToAnnex : GameManager
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneTransition();
        }
    }
    public void SceneTransition()
    {
        transferScene = "FirstAnnex";
        StartCoroutine(LoadMap());
    }
}