using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float speed = 2;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private bool canPlayerMove = true;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3Extension.AsVector2(transform.position) != targetPosition && canPlayerMove)
        {
            //Keeps the player to a grid based movement.
            isMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        }
        else
        {
            isMoving = false;
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
