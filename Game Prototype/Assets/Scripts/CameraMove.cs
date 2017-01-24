using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public GameObject Player;
    public bool camMove = false;
    public float camSpeed;

    void CamMode(bool CamMoving)
    {
        if (CamMoving)
        {
            Player.GetComponent<CharAnim>().playerMove = true;
            GetComponent<CameraFollow>().camMode = false;
            camMove = false;
        }
        else if (!CamMoving)
        {
            Player.GetComponent<CharAnim>().playerMove = false;
            GetComponent<CameraFollow>().camMode = true;
            camMove = true;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CamMode(camMove);
        }

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
