using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{

    public Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        slider.value = (float)PlayerAttacted.hp / (float)PlayerAttacted.maxHP;
    }
}
