using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Refactor names of all methods
public class UI : MonoBehaviour {

    private GameObject inGameMenuCanvas;
    private GameObject endOfLevelCanvas;
    private GameObject gameButtonsUI;

    private GameObject cameraMain;
    private GameObject player;
    private GameObject environment;

    private CharacterController characterController;
    private Camera cameraScript;

    void Start()
    {
        string name = SceneManager.GetActiveScene().name;
        int iName = 0;
        int.TryParse(name, out iName);
        if (iName !=0)
        {
            player = GameObject.FindWithTag(Tags.PLAYER);
            cameraMain = GameObject.FindWithTag(Tags.MAINCAMERA);
            inGameMenuCanvas = transform.Find(Tags.GAMEMENU).gameObject;
            endOfLevelCanvas = transform.Find(Tags.ENDLEVEL).gameObject;
            gameButtonsUI = transform.Find(Tags.BUTTONS).gameObject;
            environment = GameObject.Find(Tags.ENVIRONMENT);

            characterController = player.GetComponent<CharacterController>();
            cameraScript = cameraMain.GetComponent<Camera>();
        }
    }

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
        //Loads the main menu scene
        SceneManager.LoadScene(Tags.MAINMENU);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void SelectLevel(string level)
    {
        //Load level
        SceneManager.LoadScene(level);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void NextLevel()
    {
        AllNodes.ClearAllNodes();

        //Get the current scene name, turn it into an int and +1 for next level
        string name = SceneManager.GetActiveScene().name;
        int iName = 0;
        int.TryParse(name, out iName);
        SceneManager.LoadScene(iName+1);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }


    public void GameMenu()
    {
        //Toggles the in-game menu
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            inGameMenuCanvas.SetActive(true);
        }
        else if (inGameMenuCanvas.activeInHierarchy)
        {
            inGameMenuCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void UIGameButtons()
    {
        //Toggles visibility of UI in-game buttons
        if (gameButtonsUI.activeInHierarchy)
        {
            gameButtonsUI.SetActive(false);
        }
        else
        {
            gameButtonsUI.SetActive(true);
        }
    }

    public void LevelEnd()
    {
        //Make sure all the UI is turned off except the end level canvas
        gameButtonsUI.SetActive(false);
        inGameMenuCanvas.SetActive(false);
        endOfLevelCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void CameraMode()
    {
        cameraScript.SwitchCameraMode();
    }

    public void MovementDirection(string direction)
    {
        characterController.MobileInput(direction);
    }

    public void Reset()
    {
        characterController.ResetCharacter(environment);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameMenu();
        }
    }

}
