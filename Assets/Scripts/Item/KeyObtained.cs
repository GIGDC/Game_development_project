using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObtained : MonoBehaviour
{
    [Tooltip("�ش� Ű�� ��� ���� �ر��ϴ���")]
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
