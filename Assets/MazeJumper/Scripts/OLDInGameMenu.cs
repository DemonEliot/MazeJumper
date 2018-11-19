using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//TODO Combine with other UI scripts
// Scipts for in game menu behaviour. 

public class InGameMenu : MonoBehaviour {

    public GameObject canvas;

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        canvas.SetActive(false);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

   
}
