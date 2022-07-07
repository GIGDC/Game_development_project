using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyActiveAfterMissionDone : MonoBehaviour
{
    [Tooltip("구원/소멸시켜야 하는 유령이 inactive해지면 key 나타남")]
    public GameObject ghost;
    [SerializeField] GameObject key;
    [Tooltip("해당 열쇠가 어느 씬과 연관되어있는지")]
    [SerializeField] string sceneName;

    // Update is called once per frame
    void Update()
    {
        if(ghost.activeSelf || GameManager.openDoorList.Contains(sceneName))
            key.SetActive(false);
        else 
            key.SetActive(true);
    }
}
