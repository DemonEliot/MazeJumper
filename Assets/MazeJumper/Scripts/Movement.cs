using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Vector3 targetPosition;

    private bool isCurrentlyMoving = false;
    private bool canPlayerMove = true;
    private bool shouldPlayerMove = false;
    private float speed = 2f;

    CharacterManager characterController;
    public const int idleState = 0;
    public const int walkingState = 1;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        characterController = this.gameObject.GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfMovedPast();

        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < 0.1f)
        {
            if (isCurrentlyMoving)
            {
                return;
            }
        }
        else
       {
            if (canPlayerMove && shouldPlayerMove)
            {
                if (!isCurrentlyMoving)
                {
                    MoveTowards();
                    isCurrentlyMoving = true;
                    shouldPlayerMove = false;
                    characterController.SetAnimationState(walkingState);
                }
            }
            else
            {
                if (isCurrentlyMoving && characterController.GetIsIntangible())
                {
                    MoveTowards();
                }
            }
        }
    }

    public void MoveTowards()
    {
        float step;
        if (characterController.GetIsIntangible() && shouldPlayerMove)
        {
            step = speed * 50 * Time.deltaTime; // If you want to remove the 'jumping' between portals, remove this 50 (or the whole line).
        }
        else
        {
            step = speed * Time.deltaTime;
        }
        
        if (step > 0.8f)
        {
            step = 0.8f;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    public void RotateTowardsDirection(Vector3 directionToMove)
    {
        //The player faces the direction they move in.
        transform.forward = directionToMove;
    }

    public void SetTargetPosition(Vector2 newPosition)
    {
        targetPosition = new Vector3(newPosition.x, transform.position.y, newPosition.y);
    }

    public Vector2 GetTargetPosition()
    {
        return Vector3Extension.AsVector2(targetPosition);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public bool GetIsMoving()
    {
        return isCurrentlyMoving;
    }

    public void SetPlayerCanMove(bool state)
    {
        canPlayerMove = state;
    }

    public void SetPlayerShouldMove(bool state)
    {
        shouldPlayerMove = state;
    }

    public bool GetPlayerCanMove()
    {
        return canPlayerMove;
    }

    public bool GetPlayerShouldMove()
    {
        return shouldPlayerMove;
    }

    public void SetPlayerNotMoving()
    {
        isCurrentlyMoving = false;
        characterController.SetAnimationState(idleState);

        // Check current node for next step (Should speed up and smooth out movement)
        characterController.CheckCurrentNode();
    }

    private void CheckIfMovedPast()
    {
        if (transform.forward == Vector3.forward)
        {
            if (transform.position.z > targetPosition.z)
            {
                SetPlayerNotMoving();
                return;
            }
        }

        if (transform.forward == Vector3.back)
        {
            if (transform.position.z < targetPosition.z)
            {
                SetPlayerNotMoving();
                return;
            }
        }

        if (transform.forward == Vector3.left)
        {
            if (transform.position.x < targetPosition.x)
            {
                SetPlayerNotMoving();
                return;
            }
        }

        if (transform.forward == Vector3.right)
        {
            if (transform.position.x > targetPosition.x)
            {
                SetPlayerNotMoving();
                return;
            }
        }
    }
}
