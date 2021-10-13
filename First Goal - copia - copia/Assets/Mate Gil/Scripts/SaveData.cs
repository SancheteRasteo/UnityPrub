using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public int Equipardo = 0;
    public int Ligovich = 0;

    public string Formacao = "1-3-2";
    public string Ficharda = "Circle 4v2";


    public void saveEquipardo(int Team)
    {
        Equipardo = Team;
    }

    public void saveLigovich(int League)
    {
        Ligovich = League;
    }

    public void saveFormacao(string Form)
    {
        Formacao = Form;
    }

    public void saveFicharda(string Piece)
    {
        Ficharda = Piece;
    }
}

class InstanciasData : MonoBehaviour
{
    SaveData[] PlayerDatas = new SaveData[12];

    private void Awake()
    {
        PlayerDatas[0] = new SaveData();
        PlayerDatas[1] = new SaveData();
        PlayerDatas[2] = new SaveData();
        PlayerDatas[3] = new SaveData();
        PlayerDatas[4] = new SaveData();
        PlayerDatas[5] = new SaveData();
        PlayerDatas[6] = new SaveData();
        PlayerDatas[7] = new SaveData();
        PlayerDatas[8] = new SaveData();
        PlayerDatas[9] = new SaveData();
        PlayerDatas[10] = new SaveData();
        PlayerDatas[11] = new SaveData();
    }

    private void Start()
    {

        PlayerDatas[0].Ligovich = 3;
    }
}
