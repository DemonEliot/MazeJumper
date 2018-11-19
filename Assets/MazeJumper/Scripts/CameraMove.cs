using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;
    private Vector3 origin;
    public bool camMove = false;
    private float camSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {
        // At start, want to get the player position and position the camera with an offset to the player
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position + Vector3.up * 10 + Vector3.back * 3;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // The camera follows the player as long as camera mode isn't currently active
        if (!camMove)
        {
            transform.position = player.transform.position + offset;
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }

        if (camMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                origin = Input.mousePosition;
                return;
            }
            
            if (!Input.GetMouseButton(0))
            {
                return;
            }

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - origin);
            Vector3 move = new Vector3(pos.x * camSpeed * -1f, 0, pos.y * camSpeed * -1f);
            transform.Translate(move, Space.World);

        }
    }

    public void CamMode()
    {
        // If Camera Mode is currently on, make player able to move and camera cannot move.
        if (camMove)
        {
            player.GetComponent<CharAnim>().playerMove = true;
            camMove = false;
        }
        // If Camera Mode is currently off, make player stops being able to move and camera can move instead.
        else if (!camMove)
        {
            player.GetComponent<CharAnim>().playerMove = false;
            camMove = true;
        }
    }
	
    void CameraMovement()
    {
        // When 'cam mode' is active, moving will move the camera instaead of the player.
        if (Input.GetKey(KeyCode.RightArrow) && camMove == true)
        {
            transform.position = transform.position + Vector3.forward * camSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) && camMove == true)
        {
            transform.position = transform.position + Vector3.right * camSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && camMove == true)
        {
            transform.position = transform.position + Vector3.back * camSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow) && camMove == true)
        {
            transform.position = transform.position + Vector3.left * camSpeed * Time.deltaTime;
        }
    }

}
