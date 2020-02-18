//using UnityEngine;
//using System.Collections;

//// Should just be able to delete this class... Almost... 
//public class CubeFunctions : MonoBehaviour {

//    private Vector3 directionToFace;
//    private Vector3 targetPosition;

//    private GameObject UIContainer;

//    private void Start()
//    {
//        UIContainer = GameObject.Find("UIGameContainer");
//    }

//    void OnTriggerEnter(Collider collidedGridObject)
//    {
//        //Detects if the player collides with a "portal" and then moves them in the correct direction.
//        if (collidedGridObject.gameObject.tag == "right")
//        {
//            PortalMovement(Vector3.forward, collidedGridObject);
//            directionToFace = Vector3.forward;
//            GetComponent<CharacterController>().StopMovement();
//        }

//        else if (collidedGridObject.gameObject.tag == "down")
//        {
//            PortalMovement(Vector3.right, collidedGridObject);
//            directionToFace = Vector3.right;
//            GetComponent<CharacterController>().StopMovement();
//        }

//        else if (collidedGridObject.gameObject.tag == "left")
//        {
//            PortalMovement(Vector3.back, collidedGridObject);
//            directionToFace = Vector3.back;
//            GetComponent<CharacterController>().StopMovement();
//        }

//        else if (collidedGridObject.gameObject.tag == "up")
//        {
//            PortalMovement(Vector3.left, collidedGridObject);
//            directionToFace = Vector3.left;
//            GetComponent<CharacterController>().StopMovement();
//        }

//        //Colliding with a gate makes the player "reappear".
//        else if (collidedGridObject.gameObject.tag == "gate")
//        {
//            GetComponent<CharacterController>().SetPlayerIsIntagible(false);
//            //Just make sure that the gameobject appears in the right place...
//            GetComponent<CharacterController>().StopMovement();
//            gameObject.transform.position = new Vector3(collidedGridObject.transform.position.x, gameObject.transform.position.y, collidedGridObject.transform.position.z);
//        }

//        //will spawn the level end canvas.
//        else if (collidedGridObject.gameObject.tag == "crystal" && gameObject.GetComponent<CharacterController>().GetPlayerIsIntangible() == false)
//        {
//            UIContainer.GetComponent<UI>().LevelEnd();

//            //TODO make a less messy end of level
//        }

//    }

//    void PortalMovement(Vector3 directionToFace, Collider portal)
//    {
//        //Makes the player "intangible", moves them one square in the direction the portal faces.
//        GetComponent<CharacterController>().SetPlayerIsIntagible(true);
//        targetPosition = portal.gameObject.transform.position + directionToFace + new Vector3(0,0.5f,0);
//        //Have to update the player position, otherwise it will try to move back to the sqaure it was on.
//        GetComponent<CharacterController>().SetPlayerCurrentPosition(targetPosition);
//    }

//    void Update() {

//        //If the player is still intangible after moving one square, then they need to move again.
//        if (GetComponent<CharacterController>().GetPlayerIsIntangible() == true)
//        {
//            if (transform.position.x > targetPosition.x-0.1 && transform.position.x < targetPosition.x + 0.1 &&
//                transform.position.z > targetPosition.z - 0.1 && transform.position.z < targetPosition.z + 0.1)
//            {
//                transform.position = targetPosition;
//                targetPosition = transform.position + directionToFace;
//                GetComponent<CharacterController>().SetPlayerCurrentPosition(transform.position + directionToFace);
//            }
//            gameObject.transform.forward = directionToFace;
//        }
//    }
//}
