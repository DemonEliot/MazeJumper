using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{

    // Character transformation/rotation
    private Vector3 startPosition;
    private Quaternion startRotation;

    // Particle
    private ParticleController particleControllerScript;
    private const int particleSpeed = 4;

    // Main Camera
    private Camera cameraScript;

    // Animation
    private Animation animate;

    // Movement
    Movement movementScript;
    private const int characterSpeed = 2;

    // UI
    UI uiScript;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        particleControllerScript = this.gameObject.GetComponent<ParticleController>();

        cameraScript = GameObject.FindWithTag(Tags.MAINCAMERA).GetComponent<Camera>();

        animate = this.gameObject.GetComponent<Animation>();

        movementScript = this.gameObject.GetComponent<Movement>();

        uiScript = GameObject.FindWithTag(Tags.UI).GetComponent<UI>();
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
                        uiScript.LevelEnd();
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
        movementScript.SetSpeed(characterSpeed);
        particleControllerScript.EnableRender(true);

    }

    void ChangeToParticle()
    {
        movementScript.SetSpeed(particleSpeed);
        particleControllerScript.EnableRender(false);
    }

    // Movement for PC
    void MovementInputCheck()
    {
        // Before any movement happens, checks if there is a valid location to move to
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Up
            NodeCheck(Vector3.forward);
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

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // Right
            NodeCheck(Vector3.right);
        }
    }

    // Movement for Mobile
    public void MobileInput(string direction)
    {
        if (movementScript.GetPlayerCanMove() && !movementScript.GetIsMoving())
        {
            switch (direction)
            {
                case (Tags.UP):
                    NodeCheck(Vector3.forward);
                    break;
                case (Tags.DOWN):
                    NodeCheck(Vector3.back);
                    break;
                case (Tags.LEFT):
                    NodeCheck(Vector3.left);
                    break;
                case (Tags.RIGHT):
                    NodeCheck(Vector3.right);
                    break;
            }
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

    public void ResetCharacter(GameObject environment)
    {
        movementScript.SetTargetPosition(Vector3Extension.AsVector2(startPosition));
        transform.position = startPosition;
        transform.rotation = startRotation;
        movementScript.SetPlayerCanMove(true);
        ChangeToFlesh();
        particleControllerScript.ClearParticle();

        if (cameraScript.GetCameraMoveBool())
        {
            cameraScript.SwitchCameraMode();
        }

        foreach (Transform child in environment.transform)
        {
            child.GetComponent<Cross>().ResetCrosses();
        }
    }

    // Only want to change animation state when the character is actually there
    public void SetAnimationState(int state)
    {
            animate.SetAnimationState(state);
    }
}
