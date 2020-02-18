using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{

    // Character transformation/rotation
    private Vector3 playerStartPosition;
    // Rename to 'playerTargetPosition'
    private Vector2 playerCurrentPosition;
    private Quaternion playerStartRotation;
    // Rename to 'playerIsMoving'
    private bool playerIsWalking = false;
    private float playerSpeed = 2;
    private bool playerIsIntangible = false;
    private bool playerCanMove = true;

    // Particle use
    private SkinnedMeshRenderer[] listOfMeshRender;
    private ParticleSystem playerParticle;
    private ParticleSystem.EmissionModule playerEmmissionModule;

    private float time = 0;

    // Main Camera
    private GameObject mainCamera;
    private const string mainCameraTag = "MainCamera";
    private CameraMove cameraMoveScript;

    private PlayerAnimation animation;
    private const int idleState = 0;
    private const int walkingState = 1;

    // Use this for initialization
    void Start()
    {
        playerStartPosition = transform.position;
        playerCurrentPosition = transform.position;
        playerStartRotation = transform.rotation;

        listOfMeshRender = GetComponentsInChildren<SkinnedMeshRenderer>();
        playerParticle = GetComponentInChildren<ParticleSystem>();
        playerEmmissionModule = playerParticle.emission;

        mainCamera = GameObject.FindWithTag(mainCameraTag);
        cameraMoveScript = mainCamera.GetComponent<CameraMove>();

        animation = this.gameObject.GetComponent<PlayerAnimation>()
    }

    // Update is called once per frame
    void Update()
    {
        // Before allowing movement, need to check if the player needs to move automatically, but only if they are not currently moving
        if (!playerIsWalking)
        {
          CheckCurrentNode();
        }

        // Takes in movement input and checks for valid locations to move to
        if (time < Time.time && playerCanMove)
        {
          MovementInputCheck();
        }

        // If the player is walking and they've finished moving, then make them stop walking.
        if (playerIsWalking == true && time < Time.time)
        {
            playerIsWalking = false;
            animation.SetAnimationState(idleState);
        }

        //Keeps the player to a grid based movement.
        transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, Time.deltaTime * playerSpeed);
    }

    void CheckCurrentNode()
    {
      GameObject currentNode = AllNodes.GetNodeByPosition(Vector3Extension.AsVector2(this.transform.position));

      if (currentNode != null)
      {
        if (!playerIsIntangible)
        {
          // As a person
          switch(currentNode.tag)
          {
            case allNodes.END:
              // TODO End level if player is not a particle
              break;
            case allNodes.UP:
              ChangeTargetPosition(Vector3Extension.AsVector2(currentNode.GetNodeUp().transform.position), Vector3.forward);
              ChangePlayerToParticle();
              break;
            case allNodes.DOWN:
                ChangeTargetPosition(Vector3Extension.AsVector2(currentNode.GetNodeDown().transform.position), Vector3.back);
                ChangePlayerToParticle();
                break;
            case allNodes.LEFT:
                ChangeTargetPosition(Vector3Extension.AsVector2(currentNode.GetNodeLeft().transform.position), Vector3.left);
                ChangePlayerToParticle();
                break;
            case allNodes.RIGHT:
                ChangeTargetPosition(Vector3Extension.AsVector2(currentNode.GetNodeRight().transform.position), Vector3.right);
                ChangePlayerToParticle();
                break;

            // Should anything change if the player is standing on a floor node while flesh?
            case allNodes.GATE:
            case allNodes.START:
            case allNodes.FLOOR:
              break;
            default:
              Debug.Log("WARNING! Standing on a node with an unexpected tag");
            }
        }
        else
        {
          // As a particle
          switch(currentNode.tag)
          {
            case allNodes.UP:
              ChangeTargetPosition(Vector3Extension.AsVector2(currentNode.GetNodeUp().transform.position), Vector3.forward);
              break;
            case allNodes.DOWN:
              ChangeTargetPosition(Vector3Extension.AsVector2(currentNode.GetNodeDown().transform.position), Vector3.back);
              break;
            case allNodes.LEFT:
              ChangeTargetPosition(Vector3Extension.AsVector2(currentNode.GetNodeLeft().transform.position), Vector3.left);
              break;
            case allNodes.RIGHT:
              ChangeTargetPosition(Vector3Extension.AsVector2(currentNode.GetNodeRight().transform.position), Vector3.right);
              break;
            case allNodes.GATE:
              ChangePlayerToFlesh();

            // Should anything change if the player is standing on a floor node as a particle?
            case allNodes.START:
            case allNodes.FLOOR:
            case allNodes.END:
              break;
            default:
              Debug.Log("WARNING! Standing on a node with an unexpected tag");
          }
        }

      }
      else
      {
        Debug.Log("WARNING! Somehow standing on null space and trying to check for a node. This shouldn't be happening.");
      }

    }

    void ChangeTargetPosition(Vector2 newPosition, Vector3 direction)
    {
      playerCurrentPosition = newPosition;
      playerIsWalking = true;
      RotateTowardsDirection(direction);
      if (!playerIsIntangible)
      {
          animation.SetAnimationState(walkingState);
      }
      time = Time.time + 0.3f;
    }

    void ChangePlayerToFlesh()
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

    void ChangePlayerToParticle()
    {
      playerSpeed = 4;
      foreach (SkinnedMeshRenderer render in listOfMeshRender)
      {
          render.enabled = false;
      }
      playerEmmissionModule.enabled = true;
    }

    void MovementInputCheck()
    {
      // Before any movement happens, checks if there is a valid location to move to
      if (Input.GetKey(KeyCode.UpArrow))
      {
          // Up
          NodeCheck(Vector3.forward);
      }

      else if (Input.GetKey(KeyCode.RightArrow))
      {
          // Right
          NodeCheck(Vector3.right);
      }

      else if (Input.GetKey(KeyCode.DownArrow))
      {
          // Down
          NodeCheck(Vector3.back);
      }

      else if (Input.GetKey(KeyCode.LeftArrow))
      {
          // Left
          NodeCheck(Vector3.left);
      }
    }

    void NodeCheck(Vector3 directionToMove)
    {
        Vector2 newPosition = Vector3Extension.AsVector2(this.transform.position + directionToMove);
        if (AllNodes.GetNodeByPosition(newPosition) != null)
        {
            ChangeTargetPosition(newPosition, directionToMove);
        }
        else
        {
          RotateTowardsDirection(directionToMove);
        }
    }

    // Is this used for Mobile Movement?
    public void Movement(string direction)
    {
        switch (direction)
        {
            case ("Up"):
                //Ray(Vector3.forward);
                break;
            case ("Down"):
                //Ray(Vector3.back);
                break;
            case ("Right"):
                //Ray(Vector3.right);
                break;
            case ("Left"):
                //Ray(Vector3.left);
                break;
            default:
                break;
        }
    }

    public void StopMovement()
    {
        playerIsWalking = false;
        animation.SetAnimationState(idleState);
    }

    void RotateTowardsDirection(Vector3 directionToMove)
    {
        //The player faces the direction they move in.
        transform.forward = directionToMove;
    }

    public void ResetCharacter(GameObject environment)
    {
        // Is StopMovement being used elsewhere?
        StopMovement();
        transform.position = playerStartPosition;
        playerCurrentPosition = playerStartPosition;
        transform.rotation = playerStartRotation;
        playerIsIntangible = false;
        playerCanMove = true;
        playerParticle.Clear();

        if (cameraMoveScript.GetCameraMoveBool())
        {
            cameraMoveScript.SwitchCameraMode();
        }

        foreach (Transform child in environment.transform)
        {
            child.GetComponent<ChangeObject>().ResetCrosses();
        }
    }

    // Check if all of these getters and setters are necessary, where are they being used?
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
