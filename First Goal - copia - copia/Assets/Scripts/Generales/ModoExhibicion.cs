using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoExhibicion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (ClickMove me in FindObjectsOfType<ClickMove>())
        {
            me.ComenzarExhibicionCM();
        }

        FindObjectOfType<Turnacion>().ComenzarExhibicionT();

        FindObjectOfType<GoalTrigger>().pases = true;
    }
}
