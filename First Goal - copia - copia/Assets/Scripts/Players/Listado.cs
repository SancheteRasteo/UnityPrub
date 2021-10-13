using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listado : MonoBehaviour
{
    int id = 0;

    GameObject TheChange;

    [SerializeField]
    SaveData DataBase;

    // Player Prefabs

    [SerializeField]
    GameObject[] Players;

    // Escudos Por Ligas

    [SerializeField]
    Material[] Escudos1;

    [SerializeField]
    Material[] Escudos2;

    [SerializeField]
    Material[] Escudos3;


    private void Start()
    {

        //InstanciarPlayers();
    }


    void SetearFormaciones(string Formacion)
    {
        switch(Formacion)
        {
            case "1-3-2":
                
                switch(id)                 //     this.id
                {
                    case 1:
                    case 7:

                        this.transform.position = new Vector3(-7, 0, this.transform.position.z);

                        break;

                    case 2:
                    case 8:

                        this.transform.position = new Vector3(-4, 4, this.transform.position.z);

                        break;

                    case 3:
                    case 9:

                        this.transform.position = new Vector3(-5, 0, this.transform.position.z);

                        break;

                    case 4:
                    case 10:

                        this.transform.position = new Vector3(-4, -4, this.transform.position.z);

                        break;

                    case 5:
                    case 11:

                        this.transform.position = new Vector3(-2, 2, this.transform.position.z);

                        break;

                    case 6:
                    case 12:

                        this.transform.position = new Vector3(-2, -2, this.transform.position.z);

                        break;
                }
                break;

            case "1-4-1":


                break;

            case "1-2-2-1":


                break;
        }
    }

    void JugoEscudero(int Liga, int numEquipo)
    {
        switch (Liga)
        {
            case 1:           // Liga profesional
                        
                this.GetComponent<MeshRenderer>().material = Escudos1[numEquipo];  
                break;

            case 2:          // B Nacional

                this.GetComponent<MeshRenderer>().material = Escudos2[numEquipo];
                break;

            case 3:          // B Metro

                this.GetComponent<MeshRenderer>().material = Escudos3[numEquipo];
                break;

            case 4:          // Federal A

                this.GetComponent<MeshRenderer>().material = Escudos1[numEquipo];
                break;

            case 5:          // Primera C

                this.GetComponent<MeshRenderer>().material = Escudos1[numEquipo];
                break;

            case 6:          // Primera D

                this.GetComponent<MeshRenderer>().material = Escudos1[numEquipo];
                break;
        }
    }

    void InstanciarPlayers(string tagFicha)
    {
        foreach (GameObject playsR in GameObject.FindGameObjectsWithTag("Rival"))
        {
            Destroy(playsR);
        }

        foreach (GameObject playsL in GameObject.FindGameObjectsWithTag("Local"))
        {
            Destroy(playsL);
        }

        foreach (GameObject fichin in Players)
        {
            if(fichin.name == tagFicha)
            {
                TheChange = fichin;
            }
        }

        Instantiate(TheChange);
    }
}
