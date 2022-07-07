using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAppearController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ObtainableByClick.isFirst&&this.gameObject.name.Contains("1-1"))
            Destroy(this.gameObject);
        
    }
}
