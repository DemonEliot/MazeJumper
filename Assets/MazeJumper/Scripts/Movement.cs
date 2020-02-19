using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Vector3 targetPosition;

    private bool isMoving = false;
    private bool canPlayerMove = true;
    private float speed = 2f;

    CharacterController characterController;
    public const int idleState = 0;
    public const int walkingState = 1;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        characterController = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < 0.1f)
        {
            isMoving = false;
            transform.position = targetPosition;
            characterController.SetAnimationState(idleState);
        }
        else
        {
            if (canPlayerMove)
            {
                if (!isMoving)
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                    isMoving = true;
                    characterController.SetAnimationState(walkingState);
                }
            }
        }
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

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void SetPlayerCanMove(bool state)
    {
        canPlayerMove = state;
    }

    public bool GetPlayerCanMove()
    {
        return canPlayerMove;
    }
}
