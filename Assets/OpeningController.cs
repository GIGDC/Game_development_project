using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpeningController : MonoBehaviour
{
    public Image[] Opening;
    float ff = 1.0f;
    private void Start()
    {
        Invoke("StatringOpening", 3f);
        Invoke("OpeningEnd", 10f);
    }

    void OpeningEnd()
    {
        this.gameObject.SetActive(false);
    }
    void StatringOpening()
    {
        if (Opening[0].gameObject.activeSelf)
        {
            StartCoroutine(FadeOut(0));
        }
            
    }
    IEnumerator FadeOut(int i)
    {
        for (ff = 1.0f; ff >= 0.0f;)
        {
           ff -= 0.01f;
           Opening[i].color = new Color(0, 0, 0, ff);
           yield return new WaitForSeconds(0.1f);
        }
    }
}
