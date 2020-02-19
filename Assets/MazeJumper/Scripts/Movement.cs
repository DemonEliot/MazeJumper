using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Vector2 targetPosition;
    private bool isMoving = false;
    private bool canPlayerMove = true;
    private float speed = 2f;

    CharacterController characterController;
    public const int idleState = 0;
    public const int walkingState = 1;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = Vector3Extension.AsVector2(transform.position);
        characterController = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 characterPosition = Vector3Extension.AsVector2(transform.position);
        if (Vector2.Distance(characterPosition, targetPosition) < 0.001f)
        {
            isMoving = false;
            characterController.SetAnimationState(idleState);
        }
        else
        {
            if (canPlayerMove)
            {
                //Keeps the player to a grid based movement.
                isMoving = true;
                characterController.SetAnimationState(walkingState);
                Vector3 targetPosition3D = new Vector3(targetPosition.x, transform.position.y, targetPosition.y);
                float step = speed * Time.deltaTime;
                transform.position = transform.forward * step;
                //transform.position = Vector3.Lerp(transform.position, targetPosition3D, step);
                //transform.position = Vector3.MoveTowards(transform.position, targetPosition3D, step);
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
        targetPosition = newPosition;
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
