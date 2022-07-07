using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInGymStorage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FemaleConversation.isSuccess = true;
        this.gameObject.SetActive(false);        
    }
}
