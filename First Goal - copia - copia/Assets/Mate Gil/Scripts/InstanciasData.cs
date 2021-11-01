using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciasData : MonoBehaviour
{
    int id = 0;
    int Liga = 0;

    SaveData[] PlayerDatas = new SaveData[13];

    private void Awake()
    {
        PlayerDatas[1] = new SaveData(2, 2, "1-3-2", "Circle 5v3");
        PlayerDatas[2] = new SaveData(2, 2, "1-3-2", "Circle 4v2");
        PlayerDatas[3] = new SaveData(2, 2, "1-3-2", "Circle 3v3");
        PlayerDatas[4] = new SaveData(2, 2, "1-3-2", "Circle 3v3");
        PlayerDatas[5] = new SaveData(2, 2, "1-3-2", "Square Xv1");
        PlayerDatas[6] = new SaveData(2, 2, "1-3-2", "Circle 4v1");
        PlayerDatas[7] = new SaveData(3, 2, "1-3-2", "Circle 4v2");
        PlayerDatas[8] = new SaveData(3, 2, "1-3-2", "Circle 4v2");
        PlayerDatas[9] = new SaveData(3, 2, "1-3-2", "Circle 4v2");
        PlayerDatas[10] = new SaveData(3, 2, "1-3-2", "Circle 4v2");
        PlayerDatas[11] = new SaveData(3, 2, "1-3-2", "Circle 4v2");
        PlayerDatas[12] = new SaveData(3, 2, "1-3-2", "Circle 4v2");

        //if (PlayerPrefs.HasKey("archive"))
        //{
        //    JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("archive"), PlayerDatas[]);

        //    print("Recharge");
        //}
    }

    public void FormacionesLocal(string FormL)
    {
        print("Se guardo: " + PlayerDatas);

        for (int i = 1; i <= 6; i++)
        {
            PlayerDatas[i].Formacao = FormL;
        }
    }

    public void FormacionesRival(string FormR)
    {
        for (int i = 7; i <= 12; i++)
        {
            PlayerDatas[i].Formacao = FormR;
        }

        print("Se guardo: " + PlayerDatas[8].Formacao);
    }


    public void LigaCheck(int ligasim)
    {
        Liga = ligasim;
    }

    public void LigayEquiposLocal(int EquipoL)
    {
        for (int i = 1; i <= 6; i++)
        {
            PlayerDatas[i].Ligovich = Liga;

            PlayerDatas[i].Equipardo = EquipoL;
        }
    }

    public void LigayEquiposRival(int EquipoR)
    {
        for (int i = 7; i <= 12; i++)
        {
            PlayerDatas[i].Ligovich = Liga;

            PlayerDatas[i].Equipardo = EquipoR;
        }
    }

    
    public void IdCheck(int idsim)
    {
        id = idsim;
    }

    public void FichardasGeneral(string NombreFicha)
    {
        PlayerDatas[id].Ficharda = NombreFicha;
    }


    public void GuardarDatos()
    {
        string nombreJSON;

        for (id = 1; id <= 12; id++)
        {
            nombreJSON = "archive " + id;

            PlayerPrefs.SetString(nombreJSON, JsonUtility.ToJson(PlayerDatas[id]));
        }
    }
}
