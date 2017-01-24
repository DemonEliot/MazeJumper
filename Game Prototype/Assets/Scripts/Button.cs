using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Button : MonoBehaviour {

	public void onStart()
    {
        SceneManager.LoadScene("1");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void onMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
    
}
