using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClapController : MonoBehaviour
{
    static float time;
    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        if (time > 3.0f)
        {
            this.gameObject.SetActive(false);
            
        }
    }
}
