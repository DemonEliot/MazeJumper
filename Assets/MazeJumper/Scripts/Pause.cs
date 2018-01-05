using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public GameObject canvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                canvas.SetActive(true);
            }
            else if (canvas.activeInHierarchy)
            {
                canvas.SetActive(false);
                Time.timeScale = 1;
            }
                

        }
    }
    

}
