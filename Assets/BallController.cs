using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (this.name.Contains("�౸��"))
            {
                CountController.SocCount++;
            }
            if (this.name.Contains("�豸��"))
            {
                CountController.VolCount++;
            }
            if (this.name.Contains("�Ǳ���"))
            {
                CountController.DodCount++;
            }
            if (this.name.Contains("�󱸰�"))
            {
                CountController.BasCount++;
            }

        this.gameObject.SetActive(false);
    }
    
}
