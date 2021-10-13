using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    //Vector3 PosBall;

    bool reboot = true;
    public bool pases = false;

    int Goal_gt;

    float contFinal = 0;

    [SerializeField]
    Puntuacion score;

    [SerializeField]
    Turnacion TurnForGoals;


    // Start is called before the first frame update
    void Start()
    {
        //PosBall = GameObject.FindGameObjectWithTag("Pelota").GetComponent<Transform>().position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pelota")
        {
            if (reboot)
            {
                reboot = false;

                TurnForGoals.StopAllCoroutines();

                if(pases)
                {
                    FindObjectOfType<PasesPelota>().GoalPP = true;
                }

                foreach (ClickMove gt in FindObjectsOfType<ClickMove>())
                {
                    if(gt.haschanged)
                    {
                        gt.NoGoal = false;
                    }
                }

                Goal_gt = score.SumarGol();

                if (Goal_gt == 3)
                {
                    Goal_gt = 0;

                    TurnForGoals.ImgEnding();

                    StartCoroutine(Ending());

                    IEnumerator Ending()
                    {
                        while (contFinal <= 5)
                        {
                            yield return new WaitForSeconds(0.001f);

                            contFinal += 0.001f;
                        }
                    }

                    SceneManager.LoadScene("Menu");
                }

                TurnForGoals.Goal_t = Goal_gt;

                Goal_gt = 0;


                StartCoroutine(PausaGol());

                IEnumerator PausaGol()
                {
                    yield return new WaitForSeconds(3);

                    if (pases)
                    {
                        FindObjectOfType<PasesPelota>().GoalPP = false;
                    }

                    TurnForGoals.ReiniciarRutina();

                    /*foreach (ClickMove gt in FindObjectsOfType<ClickMove>())
                    {
                        gt.Reposicion();
                    }*/

                    //other.GetComponent<Transform>().position = PosBall;
                    //other.GetComponent<Rigidbody>().Sleep();
                    //other.GetComponent<Rigidbody>().WakeUp();

                    reboot = true;
                }
            }
        }
    }
}
