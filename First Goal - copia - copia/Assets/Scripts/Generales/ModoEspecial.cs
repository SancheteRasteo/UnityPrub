using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoEspecial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (ClickMove me in FindObjectsOfType<ClickMove>())
        {
            me.ComenzarEspecialCM();
        }

        FindObjectOfType<Turnacion>().ComenzarEspecialT();
    }
}
