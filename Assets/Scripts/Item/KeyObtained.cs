using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObtained : MonoBehaviour
{
    [Tooltip("해당 키가 어느 씬을 해금하는지")]
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            this.gameObject.SetActive(false);
            List<string> openDoorList = GameManager.openDoorList;
            if (!openDoorList.Contains(scene))
                openDoorList.Add(scene);
        }

    }
}
