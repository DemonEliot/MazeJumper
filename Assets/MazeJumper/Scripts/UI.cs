﻿using System.Collections;
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

    private CharacterManager characterController;
    private Camera cameraScript;

    private string sceneName;
    private string editedSceneName;
    private int iName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        editedSceneName = StringUtils.SplitAndTrimSceneName(sceneName);
        iName = 0;
        int.TryParse(editedSceneName, out iName);
        if (iName !=0)
        {
            player = GameObject.FindWithTag(Tags.PLAYER);
            cameraMain = GameObject.FindWithTag(Tags.MAINCAMERA);
            inGameMenuCanvas = transform.Find(Tags.GAMEMENU).gameObject;
            endOfLevelCanvas = transform.Find(Tags.ENDLEVEL).gameObject;
            gameButtonsUI = transform.Find(Tags.BUTTONS).gameObject;
            environment = GameObject.Find(Tags.ENVIRONMENT);

            characterController = player.GetComponent<CharacterManager>();
            cameraScript = cameraMain.GetComponent<Camera>();
        }
    }

    public void StartButton()
    {
        // Loads scene "1" and if the game is currently paused, sets the timescale to normal
        SceneManager.LoadScene(Tags.LEVELOFFSET);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        AllNodes.ClearAllNodes();
    }

    public void ExitButton()
    {
        // Quits the game
        Application.Quit();
    }

    public void LevelSelectButton()
    {
        //Loads the Level Select scene
        SceneManager.LoadScene(Tags.LEVELSCENE);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        AllNodes.ClearAllNodes();

    }

    public void SelectLevelButton(string level)
    {
        //Load level
        SceneManager.LoadScene(level);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void NextLevelButton()
    {
        AllNodes.ClearAllNodes();
        int nextLevel = (iName + Tags.LEVELOFFSET);

        SceneManager.LoadScene(nextLevel);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }


    public void GameMenuToggle()
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

    public void GameButtonsToggle()
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

    public void LevelEndMenu()
    {
        //Make sure all the UI is turned off except the end level canvas
        gameButtonsUI.SetActive(false);
        inGameMenuCanvas.SetActive(false);
        endOfLevelCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void CameraModeButton()
    {
        cameraScript.SwitchCameraMode();
    }

    public void MovementButtons(string direction)
    {
        characterController.MobileInput(direction);
    }

    public void ResetPlayerButton()
    {
        characterController.ResetCharacter(environment);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameMenuToggle();
        }
    }

}
