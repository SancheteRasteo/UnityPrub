using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menudepausa : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject menudepausaUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
        
            }
            else
            {
                Pause();
            }
        }
    }
   public void Resume()
    {
        menudepausaUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        foreach (GameObject aux in GameObject.FindGameObjectsWithTag("boton"))
        {
            aux.SetActive(true);
        }  
    } 
    void Pause()
    {
        menudepausaUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        foreach (GameObject aux in GameObject.FindGameObjectsWithTag("boton"))
        {
            aux.SetActive(false);
        }
       

    }
    public void LoadMenu()
    {
      
    }
    public void QuitGame()
    {
        Debug.Log("Cerrando juego...");
        Application.Quit();
     
    }
}
