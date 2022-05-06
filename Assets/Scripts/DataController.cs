using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using LitJson;
using System.IO;

public class DataController : MonoBehaviour
{
    public string dataTitle;
    public int ghostNum;
    private void Start()
    {
        
        string JsonString = File.ReadAllText(Application.dataPath + dataTitle);
        JsonData jsonData = JsonMapper.ToObject(JsonString);
        
        ParsingJsonQuest(jsonData);
    }
    private void ParsingJsonQuest(JsonData talks)
    {
        Ghost ghost = new Ghost(talks["Talk"].ToString(), talks["NegatTalk"].ToString(), talks["PositTalk"].ToString(), talks["Select1"].ToString(), talks["Select2"].ToString(), talks["Select3"].ToString());
        ActiveConversation.ghost.Add("유령 "+ghostNum, ghost); //ghost를 0번부터 시작


        foreach(Ghost g in ActiveConversation.ghost.Values)
        {
            Debug.Log(g.Talk);
        }
      
    }
}
