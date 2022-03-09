using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DownHint : MonoBehaviour
{
 
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {

            this.gameObject.SetActive(false);

        }
    }
}

