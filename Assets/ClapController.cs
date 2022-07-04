using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClapController : MonoBehaviour
{
    static float time;

    void Update()
    {

        time += Time.deltaTime;

        if (time > 3.0f)
        {
            ThreeConversation.isInputGame = true;
            this.gameObject.SetActive(false);
            
        }
    }
}
