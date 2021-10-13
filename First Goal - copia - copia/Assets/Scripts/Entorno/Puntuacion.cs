using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Puntuacion : MonoBehaviour
{
    int Goal_p = 0;
    int cont = 0;
    int MaxGoals = 3;

    [SerializeField]
    TextMeshProUGUI Marcador = new TextMeshProUGUI();


    public int SumarGol()
    {
        cont++;

        print("Anotacion");

        Marcador.text = cont.ToString();

        if (Marcador.gameObject.name == "Numero R")
        {
            Goal_p = 1;
        }
        else if(Marcador.gameObject.name == "Numero L")
        {
            Goal_p = 2;
        }

        if (cont == MaxGoals)
        {
            foreach (ClickMove p in FindObjectsOfType<ClickMove>())
            {
                p.PausaDeTiro();
            }

            Goal_p = 3;
        }

        return Goal_p;
    }
}
