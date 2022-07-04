using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissionItem : MonoBehaviour
{
    static List<string> missionItem;

    // Start is called before the first frame update
    void Awake()
    {
        if (missionItem == null)
            missionItem = new List<string>();
    }

    public string GetMissionItem(string findItem)
    {
        if (missionItem.Contains(findItem))
            return findItem;
        else
            return null;
    }

    public void AddMissionItem(string newItem)
    {
        missionItem.Add(newItem);
    }
}
