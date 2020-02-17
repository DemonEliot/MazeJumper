﻿using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

    // Variables for Animation
    private Animator animComp;
    private float animState;

    // Variables for Character transformation/rotation
    private Vector3 playerStartPosition;
    private Vector3 playerCurrentPosition;
    private Quaternion playerStartRotation;
    private bool playerIsWalking = false;
    private float playerSpeed = 2;
    private bool playerIsIntangible = false;
    private bool playerCanMove = true;

    // Variables for particle use
    private SkinnedMeshRenderer[] listOfMeshRender;
    private ParticleSystem playerParticle;
    private ParticleSystem.EmissionModule playerEmmissionModule;

    private GameObject mainCamera;
    private float time = 0;

    // Use this for initialization
    void Start () {
        animComp = this.GetComponent<Animator>();
        playerStartPosition = transform.position;
        playerCurrentPosition = transform.position;
        playerStartRotation = transform.rotation;
        listOfMeshRender = GetComponentsInChildren<SkinnedMeshRenderer>();
        playerParticle = GetComponentInChildren<ParticleSystem>();
        playerEmmissionModule = playerParticle.emission;
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    void NodeCheck()
    {
        if ()
    }

	// Update is called once per frame
	void Update ()
    {
        //Before any movement, a ray is used to detect if the player can move there.
        if (Input.GetKey (KeyCode.UpArrow) && time < Time.time && playerCanMove)
        {
            //Up
            NodeCheck();

            //Ray(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.RightArrow) && time < Time.time && playerCanMove)
        {
            //Right
            Ray(Vector3.right);
        }

        else if (Input.GetKey(KeyCode.DownArrow) && time < Time.time && playerCanMove)
        {
            //Down
            Ray(Vector3.back);
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && time < Time.time && playerCanMove)
        {
            //Left
            Ray(Vector3.left);
        }

        //If the player is walking and they've finished moving, then make them stop walking.
        if (playerIsWalking == true && time < Time.time)
        {
            playerIsWalking = false;
            animState = 0;
            animComp.SetFloat("Speed", animState);
        }

        // If the player is not teleporting through portals...
        if (!playerIsIntangible)
        {
            playerSpeed = 2;
            if (transform.position.y < 0.49)
            {
                transform.position = new Vector3(transform.position.x, 0.49f, transform.position.z);
            }
            foreach (SkinnedMeshRenderer render in listOfMeshRender)
            {
                render.enabled = true;
            }
            playerEmmissionModule.enabled = false;
        }

        else if (playerIsIntangible)
        {
            playerSpeed = 4;
            foreach (SkinnedMeshRenderer render in listOfMeshRender)
            {
                render.enabled = false;
            }
            playerEmmissionModule.enabled = true;
        }
        //Keeps the player to a grid based movement.
        transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, Time.deltaTime * playerSpeed);
    }

    public void Movement(string direction)
    {
        switch (direction)
        {
            case ("Up"):
                Ray(Vector3.forward);
                break;
            case ("Down"):
                Ray(Vector3.back);
                break;
            case ("Right"):
                Ray(Vector3.right);
                break;
            case ("Left"):
                Ray(Vector3.left);
                break;
            default:
                break;
        }
    }

    public void StopMovement()
    {
        playerIsWalking = false;
        animState = 0;
        animComp.SetFloat("Speed", animState);
    }

    void Rotate(Vector3 facing)
    {
        //The player faces the direction they move in.
        transform.forward = facing;
    }

    void Move(Vector3 moving)
    {
        //TODO change so input is ignored when moving without using time
        //The player moves one co-ordinate at a time.
        playerCurrentPosition += moving;
        playerIsWalking = true;
        animState = 1;
        animComp.SetFloat("Speed", animState);
        time = Time.time + 0.3f;
    }


    //TODO change so this no longer uses rays/collision, but tree algorithm to move.
    void Ray(Vector3 rayCheck)
    {

        if (playerIsIntangible == false)
        {
            Ray emptyCheck = new Ray(transform.position, rayCheck);

            Debug.DrawRay(transform.position, rayCheck * 1f, Color.red);
            //For everything that the raycast hits, starting from the player and going 1 square...
            foreach (RaycastHit hit in Physics.RaycastAll(emptyCheck, 1f))
            {
                //if it hits something, not including the player or floor they're standing on...
                //TODO change so uses layer mask instead
                if (hit.transform != transform && hit.transform.position != transform.position)
                {
                    Rotate(rayCheck);
                    Move(rayCheck);
                }
            }
        }
    }

    public void ResetCharacter(GameObject environment)
    {
        StopMovement();
        transform.position = playerStartPosition;
        playerCurrentPosition = playerStartPosition;
        transform.rotation = playerStartRotation;
        playerIsIntangible = false;
        playerCanMove = true;
        playerParticle.Clear();

        if (mainCamera.GetComponent<CameraMove>().GetCameraMoveBool())
        {
            mainCamera.GetComponent<CameraMove>().SwitchCameraMode();
        }

        foreach (Transform child in environment.transform)
        {
            child.GetComponent<ChangeObject>().ResetCrosses();
        }
    }

    public void SetPlayerMove(bool state)
    {
        playerCanMove = state;
    }

    public ParticleSystem GetPlayerParticle()
    {
        return playerParticle;
    }

    public bool GetPlayerIsIntangible()
    {
        return playerIsIntangible;
    }

    public void SetPlayerIsIntagible(bool state)
    {
        playerIsIntangible = state;
    }

    public Vector3 GetPlayerCurrentPosition()
    {
        return playerCurrentPosition;
    }

    public void SetPlayerCurrentPosition(Vector3 newPosition)
    {
        playerCurrentPosition = newPosition;
    }
}
