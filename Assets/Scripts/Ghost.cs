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
    public string Posit
    {

        get
        {
            return posit;
        }
    }

    public string Negat
    {

        get
        {
            return negat;
        }
    }
    public string Success
    {
        get
        {
            return select1;
        }
    }
    public string Talk
    {
        get
        {
            return talk;
        }
    }
    public string Select1
    {
        get
        {
            return select1;
        }
    }
    public string Select2
    {
        get
        {
            return select2;
        }
    }
    public string Select3
    {
        get
        {
            return select3;
        }
    }
}
