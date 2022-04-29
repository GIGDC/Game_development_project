using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataController : MonoBehaviour
{
    Dictionary<string,string> Text; //배열로 고스트 마다의 text 정리
    string id;
    string text;
    // Start is called before the first frame update
    void Start()
    {
        Text = new Dictionary<string, string>(); //ghost 3명
        /*
        var json = Resources.Load("Text/talk") as TextAsset;

        //var jlist = JsonConvert.DeserializeObject<List<DataController>>(json.text);

        //foreach (var data in jlist)
        {
            this.Text.Add(data.id, data.text);
        }*/
    }

    // Update is called once per frame
    void Update()
    {/*
        foreach (var data in Text.Keys)
        {
            Debug.Log(data);
        }
        */
    }
}
