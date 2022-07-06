using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainController : MonoBehaviour
{
    static public bool[] isMission1 = new bool[2];
    static public bool[] isMission2 = new bool[2];
    static public bool[] isMission3 = new bool[2];
    static public bool isEating = false;

    // Update is called once per frame
    private void Start()
    {
        for(int i = 0; i < 2; i++)
        {
            isMission1[i] = false;
            isMission2[i] = false; 
            isMission3[i] = false;
        }
    }

}
