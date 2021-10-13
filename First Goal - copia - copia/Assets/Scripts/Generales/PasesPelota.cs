using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasesPelota : MonoBehaviour
{
    Transform T_Shooter;

    bool UanceTimePass = false;
    public bool GoalPP = false;

    int Kicking = 1;
    int contPases = 0;
    const int maxPases = 2;
    
    const float posFinal = 0.5f;
    const float avanceANDtime = 0.01f;        // Avance de la ficha por ciclo y Definicion / Resolucion

    string ShooterTag;
    string PrimerToqueName;


    private void Update()
    {
        if(GoalPP && Kicking != 1)
        {
            if(UanceTimePass)
            {
                Destroy(this.GetComponent<FixedJoint>());
            }

            Kicking = 1;
        }
    }


    private void OnCollisionEnter(Collision Dinge)
    {
        if (!UanceTimePass && !GoalPP)
        {
            if (Dinge.collider.gameObject.tag == "Local" || Dinge.collider.gameObject.tag == "Rival")
            {
                switch (Kicking)
                {
                    case 0:
                        if (contPases < maxPases && Dinge.collider.gameObject.tag == ShooterTag && Dinge.collider.gameObject.name != PrimerToqueName)
                        {
                            this.gameObject.AddComponent<FixedJoint>().connectedBody = Dinge.collider.gameObject.GetComponent<Rigidbody>();

                            FindObjectOfType<Turnacion>().Goal_t = 4;

                            T_Shooter = Dinge.collider.gameObject.transform;

                            contPases++;
                            print("Pases: " + contPases);

                            Kicking = 1;
                            UanceTimePass = true;
                        }
                        break;

                    case 1:
                        PrimerToqueName = Dinge.collider.gameObject.name;

                        Kicking = 0;
                        break;
                }
            }
        }
    }


    public void RecibirShooterTag(string Shooter)  // Cuando Tira
    {
        ShooterTag = Shooter;
    }

    public void DestruirJoint()   // Por c/ Turno
    {
        Destroy(this.GetComponent<FixedJoint>());

        if(UanceTimePass)
        {
            StartCoroutine(AnimacionDeTiro());
        }

        UanceTimePass = false;

        Kicking = 1;
    }

    public void ReestablecerVariablesPP()    // Por c/ Turno de equipos
    {
        contPases = 0;
    }

    IEnumerator AnimacionDeTiro()
    {
        float ContPos = 0;
        Vector3 DireccionAvance = Vector3.zero;

        DireccionAvance.x = T_Shooter.position.x - this.transform.position.x;
        DireccionAvance.y = T_Shooter.position.y - this.transform.position.y;

        while (posFinal > ContPos)
        {
            yield return new WaitForSeconds(avanceANDtime);

            this.GetComponent<Rigidbody>().MovePosition(this.transform.position + (DireccionAvance * -1 * avanceANDtime));

            ContPos += avanceANDtime;
        }

        this.GetComponent<Rigidbody>().Sleep();
        this.GetComponent<Rigidbody>().WakeUp();
    }
}
