using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{

    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)PlayerAttacted.hp / PlayerAttacted.maxHP;
    }
}