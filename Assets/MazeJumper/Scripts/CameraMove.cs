using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    private GameObject player;
    private Vector3 offsetFromPlayer;
    private Vector3 clickOrigin;
    private bool canCameraMove = false;
    private readonly float cameraSpeed = 0.5f;

    // Use this for initialization
    private void Start()
    {
        // At start, want to get the player position and position the camera with an offset to the player
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position + Vector3.up * 10 + Vector3.back * 3;
        offsetFromPlayer = transform.position - player.transform.position;
        transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        // The camera follows the player as long as camera mode isn't currently active
        if (!canCameraMove)
        {
            transform.position = player.transform.position + offsetFromPlayer;
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }

        if (canCameraMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickOrigin = Input.mousePosition;
                return;
            }
            
            if (!Input.GetMouseButton(0))
            {
                return;
            }

            Vector3 position = Camera.main.ScreenToViewportPoint(Input.mousePosition - clickOrigin);
            Vector3 translationMovement = new Vector3(position.x * cameraSpeed * -1f, 0, position.y * cameraSpeed * -1f);
            transform.Translate(translationMovement, Space.World);
        }
    }
	
    private void CameraMovement()
    {
        // When 'cam mode' is active, moving will move the camera instaead of the player.
        if (Input.GetKey(KeyCode.RightArrow) && canCameraMove == true)
        {
            transform.position = transform.position + Vector3.forward * cameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) && canCameraMove == true)
        {
            transform.position = transform.position + Vector3.right * cameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && canCameraMove == true)
        {
            transform.position = transform.position + Vector3.back * cameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow) && canCameraMove == true)
        {
            transform.position = transform.position + Vector3.left * cameraSpeed * Time.deltaTime;
        }
    }

    public void SwitchCameraMode()
    {
        // If Camera Mode is currently on, make player able to move and camera cannot move.
        if (canCameraMove)
        {
            player.GetComponent<PlayerCharacter>().SetPlayerMove(true);
            canCameraMove = false;
        }
        // If Camera Mode is currently off, make player stops being able to move and camera can move instead.
        else if (!canCameraMove)
        {
            player.GetComponent<PlayerCharacter>().SetPlayerMove(false);
            canCameraMove = true;
        }
    }

    public bool GetCameraMoveBool()
    {
        return canCameraMove;
    }
}
