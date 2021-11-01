using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Turnacion : MonoBehaviour
{
    //GameObject FlechaClone;
    GameObject Ballt;
    //GameObject[] Players;

    Vector3 PosBall;

    bool GameMovement = false;
    bool TimeFinish = false;
    bool Pass1 = true;
    bool Pass2 = true;
    bool especialT = false;
    bool exhibicionT = false;

    public int Goal_t = 0;

    const float constantDelayTime = 0.00075f;
    const float constantDecreaseTime = 0.004f;
    float ThisRot_z;
    float OtherRot_z;
    float ThisPos_x;
    float OtherPos_x;
    float ThisPos_y;
    float OtherPos_y;

    string EmpiezaEste;

    [SerializeField]
    Slider TiempoTurno;

    [SerializeField]
    Image BarraImage;

    [SerializeField]
    GameObject I_Objects;

    [SerializeField]
    GameObject TechoLeft;

    [SerializeField]
    GameObject TechoRight;


    private void Awake()
    {
        Ballt = GameObject.FindGameObjectWithTag("Pelota");
        PosBall = Ballt.GetComponent<Transform>().position;

        BarraImage.color = new Color(0, 255, 0);

        int Starto = UnityEngine.Random.Range(0, 2);

        if (Starto == 1)
        {
            EmpiezaEste = "Local";
        }
        else if (Starto == 0)
        {
            EmpiezaEste = "Rival";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Keep_Music.Instance.gameObject.SetActive(false);

        //int contL = 0;
        //int contR = 6;

        //foreach (ClickMove tt_id in FindObjectsOfType<ClickMove>())
        //{
        //    if (tt_id.tag == "Local")
        //    {
        //        contL++;

        //        tt_id.id = contL;
        //    }
        //    else
        //    {
        //        contR++;

        //        tt_id.id = contR;
        //    }
        //}

        //StartCoroutine(Turnometro());
    }

    IEnumerator Turnometro()
    {
        GameMovement = false;
        Pass1 = true;
        Pass2 = true;

        TiempoTurno.value = 28;


        BorrarIndicadores();

        Ballt.GetComponent<Rigidbody>().Sleep();
        Ballt.GetComponent<Rigidbody>().WakeUp();

        Destroy(GameObject.FindGameObjectWithTag("Tirador"));
        // # Para cuando no tira y/o deja mantenida la flecha sin tirar

        switch(Goal_t)
        {
            case 0:

                if(exhibicionT)
                {
                    FindObjectOfType<PasesPelota>().ReestablecerVariablesPP();
                }

                if (EmpiezaEste == "Local")
                {
                    EmpiezaEste = "Rival";
                }
                else if (EmpiezaEste == "Rival")
                {
                    EmpiezaEste = "Local";
                }
                break;

            case 1:

                Ballt.GetComponent<Transform>().position = PosBall;

                EmpiezaEste = "Local";

                foreach (ClickMove gt in FindObjectsOfType<ClickMove>())
                {
                    gt.Reposicion();
                }
                break;

            case 2:

                Ballt.GetComponent<Transform>().position = PosBall;

                EmpiezaEste = "Rival";

                foreach (ClickMove gt in FindObjectsOfType<ClickMove>())
                {
                    gt.Reposicion();
                }
                break;

            case 4:

                Goal_t = 0;
                break;
        }
        
        foreach (ClickMove tt_nt in FindObjectsOfType<ClickMove>())
        {
            tt_nt.NoEsTuTurno(EmpiezaEste, Goal_t);
        }

        Goal_t = 0;

        InstanciarIndicadores();
        CancelarParedes();


        BarraImage.color = new Color(0, 255, 0);

        while (!GameMovement)
        {
            yield return new WaitForSeconds(constantDelayTime);

            TiempoTurno.value -= constantDecreaseTime;

            if (TiempoTurno.value < 14 && Pass1)
            {
                BarraImage.color = new Color(229, 188, 0);

                Pass1 = false;
            }

            if (TiempoTurno.value < 7 && Pass2)
            {
                BarraImage.color = new Color(197, 0, 0);

                Pass2 = false;
            }

            if (TiempoTurno.value == 0)
            {
                //print("Se acabo el turno");

                BarraImage.color = new Color(0, 255, 0);

                GameMovement = true;
                TimeFinish = true;
            }
        }

        if (TimeFinish)
        {
            if(especialT)
            {
                CambioDeBandoRandom(TimeFinish, "Rival");
            }

            ReiniciarRutina();
        }
    }

    public void Frenala()
    {
        TimeFinish = false;
        GameMovement = true;

        //print("Estan en juego");

        BorrarIndicadores();

        CancelarParedes();
    }

    public void ReiniciarRutina()
    {
        //print("Cambio de turno");

        StartCoroutine(Turnometro());
    }

    public void ImgEnding()
    {

    }


    public void BorrarIndicadores()
    {
        foreach (GameObject bi in GameObject.FindGameObjectsWithTag("Indicador"))
        {
            Destroy(bi);
        }
    }

    public void InstanciarIndicadores()
    {
        foreach (ClickMove ii in FindObjectsOfType<ClickMove>())
        {
            if (ii.Turno)
            {
                if (ii.GetComponent<MeshFilter>().sharedMesh.name == "Cylinder")
                {
                    Instantiate(ii.Indicador, ii.transform.position + new Vector3(0, 0, 0.1f), Quaternion.identity);
                }
                else     
                {
                    Instantiate(ii.Indicador, ii.transform.position + new Vector3(0, 0, 0.1f), ii.transform.rotation);
                }
            }
        }
    }

    void CancelarParedes()
    {
        if(GameMovement == false)
        {
            I_Objects.SetActive(false);
            TechoLeft.SetActive(false);
            TechoRight.SetActive(false);
        }
        else if(GameMovement == true)
        {
            I_Objects.SetActive(true);
            TechoLeft.SetActive(true);
            TechoRight.SetActive(true);
        }
    }


    public void CambioDeBandoPorTiro(ClickMove ElQueTiro)
    {
        //print(ElQueTiro.name);

        ThisPos_x = ElQueTiro.transform.position.x;
        ThisPos_y = ElQueTiro.transform.position.y;
        ThisRot_z = ElQueTiro.transform.rotation.z;

        ElQueTiro.transform.position = new Vector3(0, 8, ElQueTiro.transform.position.z);

        CambioDeBandoRandom(TimeFinish, ElQueTiro.tag);

        ElQueTiro.transform.position = new Vector3(OtherPos_x, OtherPos_y, ElQueTiro.transform.position.z);

        Quaternion.FromToRotation(new Vector3(ElQueTiro.transform.rotation.x, ElQueTiro.transform.rotation.y,
            ElQueTiro.transform.rotation.z),
            new Vector3(ElQueTiro.transform.rotation.x, ElQueTiro.transform.rotation.y, OtherRot_z));
    }

    void CambioDeBandoRandom(bool TF , string TagEQT)
    {
        //print("Random");

        int WhoChanges = 0;
        int NumTF = 0;

        if(TagEQT == "Rival")
        {
            WhoChanges = UnityEngine.Random.Range(1, 7);
            NumTF = WhoChanges;

            foreach (ClickMove cbL in FindObjectsOfType<ClickMove>())
            {
                if (cbL.tag == "Local" && cbL.id == WhoChanges)
                {
                    //print("Random: " + cbL.name);

                    if(TF)
                    {
                        ThisPos_x = cbL.transform.position.x;
                        ThisPos_y = cbL.transform.position.y;
                        ThisRot_z = cbL.transform.rotation.z;

                        TagEQT = "Local";
                    }
                    else 
                    {
                        OtherPos_x = cbL.transform.position.x;
                        OtherPos_y = cbL.transform.position.y;
                        OtherRot_z = cbL.transform.rotation.z;

                        cbL.transform.position = new Vector3(ThisPos_x, ThisPos_y, cbL.transform.position.z);

                        Quaternion.FromToRotation(new Vector3(cbL.transform.rotation.x, cbL.transform.rotation.y,
                            cbL.transform.rotation.z),
                            new Vector3(cbL.transform.rotation.x, cbL.transform.rotation.y, ThisRot_z));
                    }
                }
            }
        }
        
        if(TagEQT == "Local")
        {
            WhoChanges = UnityEngine.Random.Range(7, 13);

            foreach (ClickMove cbR in FindObjectsOfType<ClickMove>())
            {
                if (cbR.tag == "Rival" && cbR.id == WhoChanges)
                {
                    //print("Random: " + cbR.name);

                    if (TF)
                    {
                        OtherPos_x = cbR.transform.position.x;
                        OtherPos_y = cbR.transform.position.y;
                        OtherRot_z = cbR.transform.rotation.z;

                        cbR.transform.position = new Vector3(ThisPos_x, ThisPos_y, cbR.transform.position.z);

                        Quaternion.FromToRotation(new Vector3(cbR.transform.rotation.x, cbR.transform.rotation.y,
                            cbR.transform.rotation.z),
                            new Vector3(cbR.transform.rotation.x, cbR.transform.rotation.y, ThisRot_z));

                        TagEQT = "Rival";
                    }
                    else
                    {
                        OtherPos_x = cbR.transform.position.x;
                        OtherPos_y = cbR.transform.position.y;
                        OtherRot_z = cbR.transform.rotation.z;

                        cbR.transform.position = new Vector3(ThisPos_x, ThisPos_y, cbR.transform.position.z);

                        Quaternion.FromToRotation(new Vector3(cbR.transform.rotation.x, cbR.transform.rotation.y,
                            cbR.transform.rotation.z),
                            new Vector3(cbR.transform.rotation.x, cbR.transform.rotation.y, ThisRot_z));
                    }
                }
            }
        }

        if(TF)
        {
            foreach(ClickMove auxTF in FindObjectsOfType<ClickMove>())
            {
                if(auxTF.tag == "Local" && auxTF.id == NumTF)
                {
                    auxTF.transform.position = new Vector3(OtherPos_x, OtherPos_y, auxTF.transform.position.z);

                    Quaternion.FromToRotation(new Vector3(auxTF.transform.rotation.x, auxTF.transform.rotation.y,
                            auxTF.transform.rotation.z),
                            new Vector3(auxTF.transform.rotation.x, auxTF.transform.rotation.y, OtherRot_z));
                }
            }
        }
    }


    public void ComenzarEspecialT()
    {
        especialT = true;
    }

    public void ComenzarExhibicionT()
    {
        exhibicionT = true;
    }

    public void StopTheCount()
    {
        StopCoroutine(Turnometro());
    }
}
