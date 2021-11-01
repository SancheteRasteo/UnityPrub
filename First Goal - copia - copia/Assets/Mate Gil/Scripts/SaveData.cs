using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : ScriptableObject
{
    public int Equipardo;
    public int Ligovich;

    public string Formacao = "1-3-2";
    public string Ficharda = "Circle 4v2";

    public SaveData(int e, int l, string fo, string fi)
    {
        Equipardo = e;
        Ligovich = l;
        Formacao = fo;
        Ficharda = fi;
    }
}
