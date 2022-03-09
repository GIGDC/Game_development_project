using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnexText : ChangeText
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeInCoroutine());
        text.text = "º°°ü";
        first = true;
    }
}
