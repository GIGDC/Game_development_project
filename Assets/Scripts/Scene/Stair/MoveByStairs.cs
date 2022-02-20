using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveByStairs : GameManager
{
    public void SceneTransition(string sceneName)
    {
        transitionAnimator = GetTransitionAnimator();
        transferScene = sceneName;
        StartCoroutine(FadeOut());
    }
}

