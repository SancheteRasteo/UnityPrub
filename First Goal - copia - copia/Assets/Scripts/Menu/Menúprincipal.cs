using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menúprincipal : MonoBehaviour
{
    [SerializeField]
    GameObject Jugar;


    public void VolverEpico()
    {
        Jugar.SetActive(true);

        this.gameObject.SetActive(false);
    }

    public void SaliR()
    {
        Application.Quit();
    }
}