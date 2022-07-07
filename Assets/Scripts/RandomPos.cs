using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float min = -3.0f;
        float max = 3.0f;
        float randomX = Random.Range(min, max);
        float randomY = Random.Range(min, max);
        this.transform.position = new Vector3(-11f,-16f, 0);
    }
}
