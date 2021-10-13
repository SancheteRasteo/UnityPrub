using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erpocisionimgen : MonoBehaviour
{
    [SerializeField]
    GameObject form;
    // Start is called before the first frame update
   
   public void position ()
    {
        foreach(GameObject form in GameObject.FindGameObjectsWithTag("Azul form"))
        {
            if (form.transform.position == new Vector3(23.5f, 5, 0))
            {
                form.transform.position = new Vector3(45, 5, 0);
            }
        }

        form.transform.position= new Vector3(23.5f, 5, 0);
    }
}
