using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flechazo : MonoBehaviour
{
    float Extremo = Convert.ToSingle(5);
    float LargoEscalado;

    [SerializeField]
    GameObject PunteroFlecha;

    [SerializeField]
    GameObject PuntoCirc;


    public void AcomodarFlecha(float Num)
    {
        PuntoCirc.transform.position = new Vector3(-Num, PuntoCirc.transform.position.y, PuntoCirc.transform.position.z);
    }

    public void Redimension(float LargoReal)
    {
        if(LargoReal <= Extremo)
        {
            LargoEscalado = LargoReal / Extremo;
        }
        else
        {
            LargoEscalado = 1;
        }

        PuntoCirc.transform.localScale = new Vector3(LargoEscalado, LargoEscalado, 1);
    }
}
