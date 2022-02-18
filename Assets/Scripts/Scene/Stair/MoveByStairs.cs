using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveByStairs : GameManager
{
    int cnt = 0;
    private Button firstFloorBtn;

    private void Start()
    {
        animator = GetAnimator();
        firstFloorBtn = GameObject.Find("1F").GetComponent<Button>();
        firstFloorBtn.onClick.AddListener(SceneTransition);
    }

    private void SceneTransition()
    {
        transferScene = "1F";
        StartCoroutine(FadeOut());
    }
}

