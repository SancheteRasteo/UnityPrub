using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

public class ClickMove : MonoBehaviour
{
    Rigidbody RB;

    GameObject FlechaClone;
    public GameObject Indicador;
    GameObject Ballcm;

    Vector3 PelotaTransform;
    Vector3 PlayerTransform;
    Vector3 Mouse;
    Vector3 InicialPos;

    Quaternion InicialRot;

    bool UnderPressure;
    public bool Turno;
    bool Allows = true;
    public bool haschanged;                 // ¡ Este se deshabilita antes para No seguir con Update
    bool seleccionado = false;
    bool especialCM = false;
    bool esteTiro = false;           // ¡
    bool PrimeraVez = true;
    bool SeFrenoP;
    public bool NoGoal = true;
    bool exhibicion = false;
    bool CambioBall = false;

    public int id = 0;

    float limitShot = 4.5F;
    float Y;
    float X;
    float RadioObjeto;
    float TiroAngle;
    const float FrameTime = 0.0192f;

    [SerializeField]
    float Speed;

    [SerializeField]
    Turnacion turn;

    [SerializeField]
    GameObject Flecha;

    [SerializeField]
    Camera MainCamera;

    [SerializeField]
    Flechazo FlechaScript;

    private void Awake()
    {
        Ballcm = GameObject.FindGameObjectWithTag("Pelota");

        InicialPos = this.transform.position;
        InicialRot = this.transform.rotation;

        RadioObjeto = this.transform.lossyScale.y / 2;

        turn = FindObjectOfType<Turnacion>();
        MainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (haschanged && !NoGoal)
        {
            haschanged = false;
            NoGoal = true;
        }
    }


    IEnumerator Spectator()
    {
        while(haschanged && NoGoal)
        {
            if (SeFrenoP)
            {
                PelotaTransform = Ballcm.transform.position;

                CambioBall = true;
            }
            else
            {
                PlayerTransform = this.transform.position;
            }

            yield return new WaitForSeconds(FrameTime);

            if (Mathf.Clamp(this.transform.position.x - PlayerTransform.x, -0.005f, 0.005f) == this.transform.position.x - PlayerTransform.x
                && Mathf.Clamp(this.transform.position.y - PlayerTransform.y, -0.005f, 0.005f) == this.transform.position.y - PlayerTransform.y || SeFrenoP)
            {
                SeFrenoP = true;

                if (Mathf.Clamp(Ballcm.transform.position.x - PelotaTransform.x, -0.0085f, 0.0085f) == Ballcm.transform.position.x - PelotaTransform.x
                    && Mathf.Clamp(Ballcm.transform.position.y - PelotaTransform.y, -0.0085f, 0.0085f) == Ballcm.transform.position.y - PelotaTransform.y && CambioBall)
                {
                    haschanged = false;

                    if (exhibicion)
                    {
                        FindObjectOfType<PasesPelota>().DestruirJoint();
                    }

                    turn.ReiniciarRutina();
                    //print("Se freno " + this.name);
                    //print(this.transform + " es igual a " + PlayerTransform);
                }
            }
        }
    }


    private void OnMouseDown()
    {
        if (Allows && Turno)
        {
            UnderPressure = true;
        }
    }

    private void OnMouseDrag()
    {
        if (Allows && Turno)
        {
            seleccionado = true;

            if (!UnderPressure)
            {
                Mouse = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                Mouse.z = 0;

                Vector3 Miracion = Mouse - FlechaClone.transform.position;
                Miracion.z = 0;
                Miracion.y += 2.5f;

                FlechaClone.transform.right = Miracion * -1;

                FlechaClone.GetComponent<Flechazo>().Redimension(Miracion.magnitude - RadioObjeto);
            }
        }
    }

    private void OnMouseExit()
    {
        if (seleccionado && Allows && Turno)
        {
            UnderPressure = false;

            FlechaScript.AcomodarFlecha(RadioObjeto);

            Instantiate(Flecha, this.transform.position, InicialRot);

            FlechaClone = GameObject.FindGameObjectWithTag("Tirador");

            turn.BorrarIndicadores();
        }
    }

    private void OnMouseOver()
    {
        if (seleccionado && !UnderPressure && Allows && Turno)
        {
            UnderPressure = true;

            turn.InstanciarIndicadores();
            Destroy(FlechaClone);
        }
    }

    private void OnMouseUp()
    {
        seleccionado = false;

        if (!UnderPressure && Turno && Allows)
        {
            TiroAngle = FlechaClone.transform.rotation.eulerAngles.z;

            Destroy(FlechaClone);            // # Para cuando tira y se debe frenar todo

            esteTiro = true;
            SeFrenoP = false;
            CambioBall = false;
            haschanged = true;

            StartCoroutine(Spectator());

            if (exhibicion)
            {
                FindObjectOfType<PasesPelota>().RecibirShooterTag(this.tag);
            }

            turn.Frenala();

            /*if(especial)                  //Aca funciona pero no me gusta que lo haga al toque y 
            CambioDeBando();              //no lo hace cuando se termina el turno
            */

            Mouse = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Mouse.z = 0;

            X = Mouse.x - transform.position.x;
            Y = Mouse.y - transform.position.y + 2.5f;

            Mouse.x = -1 * X;
            Mouse.y = -1 * Y;

            if (Mouse.magnitude > limitShot)
            {
                Mouse.x = -limitShot * 2 * Mathf.Cos((TiroAngle + 180) * Mathf.Deg2Rad);
                Mouse.y = -limitShot * 2 * Mathf.Sin((TiroAngle + 180) * Mathf.Deg2Rad);
            }

            Mouse.z = 0;

            RB.AddForce(Mouse * Speed);

            PausaDeTiro();
        }
    }


    public void Reposicion()
    {
        transform.rotation = InicialRot;
        transform.position = InicialPos;
        RB.Sleep();
        RB.WakeUp();

        //print(this.name + " : " + this.tag + " , " + this.transform.position.x);
    }

    public void NoEsTuTurno(string OneWhoPlays, int Goal)
    {
        if(PrimeraVez)
        {
            RB = GetComponent<Rigidbody>();
        }

        //print(this.name + " : " + PrimeraVez);

        if (Goal == 0 || PrimeraVez)
        {
            RB.Sleep();           
            RB.WakeUp();
            
            PrimeraVez = false;
        }

        if (especialCM && esteTiro && Goal == 0)
        {
            turn.CambioDeBandoPorTiro(this);
        }

        esteTiro = false;
        Allows = true;

        if (this.tag == OneWhoPlays)
        {
            Turno = true;
        }
        else
        {
            Turno = false;
        }
    }

    /*void CambioDeBandoPorTiro()         //Modo alternativo con cambio de bandos en vez de posiciones
    {
        //print("La meti");

        int WhoChanges;

        if (this.tag == "Local")
        {
            ThisPos = this.transform.position;
            ThisRot = this.transform.rotation;

            CambioDeBandoRandom();

            WhoChanges = UnityEngine.Random.Range(7, 13);

            foreach (ClickMove cbR in FindObjectsOfType<ClickMove>())
            {
                if (cbR.tag == "Rival" && cbR.id == WhoChanges)
                {
                    print("Random: " + cbR.name);

                    OtherPos = cbR.transform.position;
                    OtherRot = cbR.transform.rotation;

                    cbR.transform.position = ThisPos;
                    cbR.transform.rotation = ThisRot;
                }
            }

            this.transform.position = OtherPos;
            this.transform.rotation = OtherRot;
        }
        else if (this.tag == "Rival")
        {
            ThisPos = this.transform.position;
            ThisRot = this.transform.rotation;

            WhoChanges = UnityEngine.Random.Range(1, 7);

            foreach (ClickMove cbL in FindObjectsOfType<ClickMove>())
            {
                if (cbL.tag == "Local" && cbL.id == WhoChanges)
                {
                    print("Random: " + cbL.name);

                    OtherPos = cbL.transform.position;
                    OtherRot = cbL.transform.rotation;

                    cbL.transform.position = ThisPos;
                    cbL.transform.rotation = ThisRot;
                }
            }

            this.transform.position = OtherPos;
            this.transform.rotation = OtherRot;
        }

        esteTiro = false;
    }

    public void CambioDeBandoRandom()
    {
        int WhoChanges;

        WhoChanges = UnityEngine.Random.Range(7, 13);

        foreach (ClickMove cbR in FindObjectsOfType<ClickMove>())
        {
            if (cbR.tag == "Rival" && cbR.id == WhoChanges)
            {
                print("Random: " + cbR.name);

                OtherPos = cbR.transform.position;
                OtherRot = cbR.transform.rotation;

                cbR.transform.position = ThisPos;
                cbR.transform.rotation = ThisRot;
            }
        }
    }*/


    public void PausaDeTiro()
    {
        Allows = false;
    } 

    public void ComenzarEspecialCM()
    {
        especialCM = true;
    }

    public void ComenzarExhibicionCM()
    {
        exhibicion = true;
    }
}