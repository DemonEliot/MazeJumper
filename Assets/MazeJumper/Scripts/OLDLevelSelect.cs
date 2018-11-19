using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//TODO  Combine with other UI scripts

public class LevelSelect : MonoBehaviour {

	public void SelectLevel(int level)
    {
        SceneManager.LoadScene(level);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
    
}
