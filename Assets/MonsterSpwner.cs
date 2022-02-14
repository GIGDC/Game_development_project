using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwner : MonoBehaviour
{
    public GameObject prefabs;
    GameObject Monster;
    // Start is called before the first frame update
    void Start()
    {
        TransferMap.CheckMonster = false;
        prefabs.SetActive(true);

        int RandomX = UnityEngine.Random.Range(-24, 35);
        int RandomY = UnityEngine.Random.Range(24, -5);

        Monster = Instantiate(prefabs, new Vector2(RandomX, RandomY), Quaternion.identity);
       
    }

}
