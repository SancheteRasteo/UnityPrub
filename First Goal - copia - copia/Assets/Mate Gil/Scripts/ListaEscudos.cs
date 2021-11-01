using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaEscudos : MonoBehaviour
{
    Material EscudoLocal;
    Material EscudoRival;

    [SerializeField]
    Material[] Escudos1;             // Liga Profesional

    [SerializeField]
    Material[] Escudos2;             // B Nacional

    [SerializeField]
    Material[] Escudos3;             // B Metropolitana

    [SerializeField]
    Material[] Escudos4;             // Federal A

    [SerializeField]
    Material[] Escudos5;             // Primera C

    [SerializeField]
    Material[] Escudos6;             // Primera D


    void CambiarEscudosDemos(int Liga, int numEquipo, string SideTag)
    {
        switch (Liga)
        {
            case 1:           // Liga profesional

                GameObject.FindGameObjectWithTag(SideTag).GetComponent<MeshRenderer>().material = Escudos1[numEquipo];
                break;

            case 2:          // B Nacional

                GameObject.FindGameObjectWithTag(SideTag).GetComponent<MeshRenderer>().material = Escudos2[numEquipo];
                break;

            case 3:          // B Metro

                GameObject.FindGameObjectWithTag(SideTag).GetComponent<MeshRenderer>().material = Escudos3[numEquipo];
                break;

            case 4:          // Federal A

                GameObject.FindGameObjectWithTag(SideTag).GetComponent<MeshRenderer>().material = Escudos4[numEquipo];
                break;

            case 5:          // Primera C

                GameObject.FindGameObjectWithTag(SideTag).GetComponent<MeshRenderer>().material = Escudos5[numEquipo];
                break;

            case 6:          // Primera D

                GameObject.FindGameObjectWithTag(SideTag).GetComponent<MeshRenderer>().material = Escudos6[numEquipo];
                break;
        }
    }
}
