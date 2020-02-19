using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{

    // Character transformation/rotation
    private Vector3 startPosition;
    private Quaternion startRotation;

    // Particle
    private ParticleController particleControllerScript;

    // Main Camera
    private GameObject cameraObject;
    private Camera cameraScript;

    // Animation
    private Animation animate;

    // Movement
    Movement movementScript;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        particleControllerScript = this.gameObject.GetComponent<ParticleController>();

        cameraObject = GameObject.FindWithTag(Tags.MAINCAMERA);
        cameraScript = cameraObject.GetComponent<Camera>();

        animate = this.gameObject.GetComponent<Animation>();

        movementScript = this.gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Before allowing movement, need to check if the player needs to move automatically, but only if they are not currently moving
        if (!movementScript.GetIsMoving())
        {
            CheckCurrentNode();
        }

        // Takes in movement input and checks for valid locations to move to
        if (movementScript.GetPlayerCanMove() && !movementScript.GetIsMoving())
        {
            MovementInputCheck();
        }
    }

    void CheckCurrentNode()
    {
        if (AllNodes.DoesDictionaryContainKey(Vector3Extension.AsVector2(this.transform.position)))
        { 
            GameObject currentNodeObject = AllNodes.GetNodeByPosition(Vector3Extension.AsVector2(this.transform.position));
            Node currentNodeScript = currentNodeObject.GetComponent<Node>();

            if (!particleControllerScript.GetIsIntangible())
            {
                // As a person
                switch (currentNodeObject.tag)
                {
                    case Tags.END:
                        // TODO End level if player is not a particle
                        // UIContainer.GetComponent<UI>().LevelEnd();
                        break;
                    case Tags.UP:
                        ChangeTargetPosition(Vector3Extension.AsVector2(currentNodeScript.GetNodeUp().transform.position), Vector3.forward);
                        ChangeToParticle();
                        break;
                    case Tags.DOWN:
                        ChangeTargetPosition(Vector3Extension.AsVector2(currentNodeScript.GetNodeDown().transform.position), Vector3.back);
                        ChangeToParticle();
                        break;
                    case Tags.LEFT:
                        ChangeTargetPosition(Vector3Extension.AsVector2(currentNodeScript.GetNodeLeft().transform.position), Vector3.left);
                        ChangeToParticle();
                        break;
                    case Tags.RIGHT:
                        ChangeTargetPosition(Vector3Extension.AsVector2(currentNodeScript.GetNodeRight().transform.position), Vector3.right);
                        ChangeToParticle();
                        break;
                }
            }
            else
            {
                // As a particle
                switch (currentNodeObject.tag)
                {
                    case Tags.UP:
                        ChangeTargetPosition(Vector3Extension.AsVector2(currentNodeScript.GetNodeUp().transform.position), Vector3.forward);
                        break;
                    case Tags.DOWN:
                        ChangeTargetPosition(Vector3Extension.AsVector2(currentNodeScript.GetNodeDown().transform.position), Vector3.back);
                        break;
                    case Tags.LEFT:
                        ChangeTargetPosition(Vector3Extension.AsVector2(currentNodeScript.GetNodeLeft().transform.position), Vector3.left);
                        break;
                    case Tags.RIGHT:
                        ChangeTargetPosition(Vector3Extension.AsVector2(currentNodeScript.GetNodeRight().transform.position), Vector3.right);
                        break;
                    case Tags.GATE:
                        ChangeToFlesh();
                        break;
                }
            }
        }
    }

    void ChangeTargetPosition(Vector2 newPosition, Vector3 direction)
    {
        movementScript.SetTargetPosition(newPosition);
        movementScript.RotateTowardsDirection(direction);
    }

    void ChangeToFlesh()
    {
        movementScript.SetSpeed(2);
        particleControllerScript.EnableRender(true);
    }

    void ChangeToParticle()
    {
        movementScript.SetSpeed(4);
        particleControllerScript.EnableRender(false);
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
        if (AllNodes.DoesDictionaryContainKey(newPosition))
        {
            ChangeTargetPosition(newPosition, directionToMove);
        }
        else
        {
            movementScript.RotateTowardsDirection(directionToMove);
        }
    }

    public void MobileInput(string direction)
    {
        //    switch (direction)
        //    {
        //        case ("Up"):
        //            //Ray(Vector3.forward);
        //            break;
        //        case ("Down"):
        //            //Ray(Vector3.back);
        //            break;
        //        case ("Right"):
        //            //Ray(Vector3.right);
        //            break;
        //        case ("Left"):
        //            //Ray(Vector3.left);
        //            break;
        //        default:
        //            break;
        //    }
    }

    public void ResetCharacter(GameObject environment)
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        movementScript.SetTargetPosition(startPosition);
        movementScript.SetPlayerCanMove(true);
        ChangeToFlesh();
        particleControllerScript.ClearParticle();

        if (cameraScript.GetCameraMoveBool())
        {
            cameraScript.SwitchCameraMode();
        }

        foreach (Transform child in environment.transform)
        {
            child.GetComponent<ChangeObject>().ResetCrosses();
        }
    }

    // Only want to change animation state when the character is actually there
    public void SetAnimationState(int state)
    {
        if (!particleControllerScript.GetIsIntangible())
        {
            animate.SetAnimationState(state);
        }
    }
}
