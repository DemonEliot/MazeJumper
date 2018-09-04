using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//TODO Combine with other UI scripts

public class Button : MonoBehaviour {

	public void OnStart()
    {
        // Loads scene "1" and if the game is currently paused, sets the timescale to normal
        SceneManager.LoadScene("1");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void OnExit()
    {
        // Quits the game
        Application.Quit();
    }

    public void OnMainMenu()
    {
        // Loads the main menu and if the game is currently paused, sets the timescale to normal
        SceneManager.LoadScene("Main Menu");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
    
}
