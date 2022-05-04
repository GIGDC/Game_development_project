using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using LitJson;
using System.IO;

public class DataController : MonoBehaviour
{
    private void Start()
    {
        
        string JsonString = File.ReadAllText(Application.dataPath + "/Text/talk.json");
        JsonData jsonData = JsonMapper.ToObject(JsonString);
        if (ActiveConversation.ghost == null)
        {
            Debug.Log(">>>>>>>>>>>>>");
        }
        ParsingJsonQuest(jsonData);
    }
    private void ParsingJsonQuest(JsonData talks)
    {
        int i = 0;
        foreach(JsonData talk in talks["Ghost"])
        {
            Ghost ghost = new Ghost(talk["Talk"].ToString(), talk["NegatTalk"].ToString(), talk["PositTalk"].ToString(), talk["Select1"].ToString(), talk["Select2"].ToString(), talk["Select3"].ToString());
            //ActiveConversation.ghost.Add(i,ghost); //ghost를 0번부터 시작
            i++;
        }
    }
}
