using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using LitJson;
using System.IO;

//미리 Setting
public class DataController : MonoBehaviour
{
    public static Dictionary<int, Ghost> ghosts;
    private void Start()
    {
        ghosts = new Dictionary<int, Ghost>();
        string JsonString1 = File.ReadAllText(Application.dataPath + "/Text/Ghost1.json");
        string JsonString2 = File.ReadAllText(Application.dataPath + "/Text/Ghost2.json");
        string JsonString3 = File.ReadAllText(Application.dataPath + "/Text/Ghost3.json");
        JsonData jsonData1 = JsonMapper.ToObject(JsonString1);
        JsonData jsonData2 = JsonMapper.ToObject(JsonString2);
        JsonData jsonData3 = JsonMapper.ToObject(JsonString3);
        ParsingJsonQuest(jsonData1, 1);
        ParsingJsonQuest(jsonData2, 2);
        ParsingJsonQuest(jsonData3, 3);
    }
    private void ParsingJsonQuest(JsonData talks, int ghostNum)
    {
        Ghost ghost = new Ghost(talks["Talk"].ToString(), talks["NegatTalk"].ToString(), talks["PositTalk"].ToString(), talks["Select1"].ToString(), talks["Select2"].ToString(), talks["Select3"].ToString());

        if (ghosts != null)
            ghosts.Add(ghostNum, ghost); //ghost를 0번부터 시작
        else
        {
            ghosts = new Dictionary<int, Ghost>();
            ghosts.Add(ghostNum, ghost); //ghost를 0번부터 시작
        }

    }
}