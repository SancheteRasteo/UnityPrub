using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keep_Music : MonoBehaviour
{
    private static Keep_Music instance;

    bool FirstTimeLoad = false;

    [SerializeField]
    Menúprincipal Minu;


    public static Keep_Music Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(Keep_Music.instance == null)
        {
            Keep_Music.instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if(SceneManager.GetActiveScene().name == "Menu")
        {
            if (!Keep_Music.instance.FirstTimeLoad)
            {
                Keep_Music.instance.FirstTimeLoad = true;
            }
            else
            {
                Minu.VolverEpico();
            }
        }

        if(!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
