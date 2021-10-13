using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class salir : MonoBehaviour
{
    public void inicio(string Escena)
    {
        SceneManager.LoadScene(Escena);
    }
}
