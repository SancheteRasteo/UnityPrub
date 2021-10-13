using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public void LoadSceneN(string SceneN)
    {
        SceneManager.LoadScene(SceneN);
    }
}
