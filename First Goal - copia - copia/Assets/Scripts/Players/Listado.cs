using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listado : MonoBehaviour
{
    int id = 0;

    SaveData[] TheChange = new SaveData[13];

    GameObject[] Teams = new GameObject[13];

    [SerializeField]
    Transform Padre;

    [SerializeField]
    Turnacion Tu;

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


    private void Awake()
    {
        string nombreJSON;

        for (id = 1; id <= 12; id++)
        {
            TheChange[id] = new SaveData(2, 2, "1-3-2", "Circle 4v2");

            nombreJSON = "archive " + id;

            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(nombreJSON), TheChange[id]);
        }
    }
    
    private void Start()
    {
        foreach (GameObject playsR in GameObject.FindGameObjectsWithTag("Rival"))
        {
            Destroy(playsR);
        }

        foreach (GameObject playsL in GameObject.FindGameObjectsWithTag("Local"))
        {
            Destroy(playsL);
        }

        for (id = 1; id <= 12; id++)
        {
            SelectPlayers();

            if (id < 7)
            {
                Teams[id].tag = "Local";
            }
            else if(id > 6)
            {
                Teams[id].tag = "Rival";
            }

            SetearFormaciones();
            JugoEscudero();
            InstanciarPlayers();
        }

        Tu.ReiniciarRutina();
    }


    void SelectPlayers()
    {
        foreach (GameObject player in Players)
        {
            if(player.name == TheChange[id].Ficharda)
            {
                Teams[id] = player;

                Teams[id].GetComponent<ClickMove>().id = id;
            }
        }
    }

    void SetearFormaciones()
    {
        switch (TheChange[id].Formacao)
        {
            case "1-3-2":

                switch (id)                 //     this.id
                {
                    case 1:

                        Teams[id].transform.position = new Vector3(-8, 0, Teams[id].transform.position.z);

                        break;

                    case 2:

                        Teams[id].transform.position = new Vector3(-4, 4, Teams[id].transform.position.z);

                        break;

                    case 3:

                        Teams[id].transform.position = new Vector3(-6, 0, Teams[id].transform.position.z);

                        break;

                    case 4:

                        Teams[id].transform.position = new Vector3(-4, -4, Teams[id].transform.position.z);

                        break;

                    case 5:

                        Teams[id].transform.position = new Vector3(-2, 1, Teams[id].transform.position.z);

                        break;

                    case 6:

                        Teams[id].transform.position = new Vector3(-2, -1, Teams[id].transform.position.z);

                        break;

                    case 7:

                        Teams[id].transform.position = new Vector3(8, 0, Teams[id].transform.position.z);

                        break;

                    case 8:

                        Teams[id].transform.position = new Vector3(4, -4, Teams[id].transform.position.z);

                        break;

                    case 9:

                        Teams[id].transform.position = new Vector3(6, 0, Teams[id].transform.position.z);

                        break;

                    case 10:

                        Teams[id].transform.position = new Vector3(4, 4, Teams[id].transform.position.z);

                        break;

                    case 11:

                        Teams[id].transform.position = new Vector3(2, -1, Teams[id].transform.position.z);

                        break;

                    case 12:

                        Teams[id].transform.position = new Vector3(2, 1, Teams[id].transform.position.z);

                        break;
                }
                break;

            case "1-4-1":

                switch (id)
                {
                    case 1:

                        Teams[id].transform.position = new Vector3(-9, 0, Teams[id].transform.position.z);

                        break;

                    case 2:

                        Teams[id].transform.position = new Vector3(-5, 3, Teams[id].transform.position.z);

                        break;

                    case 3:

                        Teams[id].transform.position = new Vector3(-6, 1, Teams[id].transform.position.z);

                        break;

                    case 4:

                        Teams[id].transform.position = new Vector3(-6, -1, Teams[id].transform.position.z);

                        break;

                    case 5:

                        Teams[id].transform.position = new Vector3(-5, -3, Teams[id].transform.position.z);

                        break;

                    case 6:

                        Teams[id].transform.position = new Vector3(-3, 0, Teams[id].transform.position.z);

                        break;

                    case 7:

                        Teams[id].transform.position = new Vector3(9, 0, Teams[id].transform.position.z);

                        break;

                    case 8:

                        Teams[id].transform.position = new Vector3(5, -3, Teams[id].transform.position.z);

                        break;

                    case 9:

                        Teams[id].transform.position = new Vector3(6, -1, Teams[id].transform.position.z);

                        break;

                    case 10:

                        Teams[id].transform.position = new Vector3(6, 1, Teams[id].transform.position.z);

                        break;

                    case 11:

                        Teams[id].transform.position = new Vector3(5, 3, Teams[id].transform.position.z);

                        break;

                    case 12:

                        Teams[id].transform.position = new Vector3(3, 0, Teams[id].transform.position.z);

                        break;
                }

                break;

            case "1-2-2-1":


                break;
        }
    }

    void JugoEscudero()
    {
        int numEquipo = TheChange[id].Equipardo;

        switch (TheChange[id].Ligovich)
        {
            case 1:           // Liga profesional

                Teams[id].GetComponent<MeshRenderer>().material = Escudos1[numEquipo];
                break;

            case 2:          // B Nacional

                Teams[id].GetComponent<MeshRenderer>().material = Escudos2[numEquipo];
                break;

            case 3:          // B Metro

                Teams[id].GetComponent<MeshRenderer>().material = Escudos3[numEquipo];
                break;

            case 4:          // Federal A

                Teams[id].GetComponent<MeshRenderer>().material = Escudos1[numEquipo];
                break;

            case 5:          // Primera C

                Teams[id].GetComponent<MeshRenderer>().material = Escudos1[numEquipo];
                break;

            case 6:          // Primera D

                Teams[id].GetComponent<MeshRenderer>().material = Escudos1[numEquipo];
                break;
        }
    }

    void InstanciarPlayers()
    {
        Instantiate(Teams[id], Teams[id].transform.position, Teams[id].transform.rotation, Padre);

        //for (id = 1; id <= 12; id++)
        //{
        //    print(Teams[id].transform.position);


        //}

        FindObjectOfType<ClickMove>(FindObjectOfType<ClickMove>().id == id).gameObject.name = Teams[id].tag + " " + id;
    }
}
