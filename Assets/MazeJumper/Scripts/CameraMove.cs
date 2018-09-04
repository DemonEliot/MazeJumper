using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    // Need to think about how to do camera movement for mobile. Currently we press a button and then can move the camera. This could be done by pressing a camera button on screen or something similar.
    // However, intuitively most players are going to want to be able to move the camera just by dragging their finger across the screen, or in a like manner...   

    public GameObject player;
    private Vector3 offset;
    public bool camMove = false;
    public float camSpeed;

    // Use this for initialization
    void Start()
    {
        // At start, want to get the player position and position the camera with an offset to the player
        transform.position = player.transform.position + Vector3.up * 10;
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
    }

    void CamMode(bool CamMoving)
    {
        // If Camera Mode is currently on, make player able to move and camera cannot move.
        if (CamMoving)
        {
            player.GetComponent<CharAnim>().playerMove = true;
            camMove = false;
        }
        // If Camera Mode is currently off, make player stops being able to move and camera can move instead.
        else if (!CamMoving)
        {
            player.GetComponent<CharAnim>().playerMove = false;
            camMove = true;
        }
    }
	
	// Update is called once per frame
	void Update () {

        // Pressing a button (currently space) will activate and disactivate 'cam mode.'
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CamMode(camMove);
        }

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
