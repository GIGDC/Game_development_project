using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost
{
    string talk;
    string negat;
    string posit;

    string select1;
    string select2;
    string select3;

    public Ghost(string talk,string negat,string posit,string select1,string select2,string select3)
    {
        this.talk = talk;
        this.negat = negat;
        this.posit = posit;
        this.select1 = select1;
        this.select2 = select2;
        this.select3 = select3;
    }

    public string Talk
    {
        get
        {
            return talk;
        }
    }
}
