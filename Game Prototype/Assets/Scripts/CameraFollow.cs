using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    public bool camMode = false;

    // Use this for initialization
    void Start () {
        transform.position = player.transform.position + Vector3.up * 10;
        offset = transform.position - player.transform.position;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (!camMode)
        {
            transform.position = player.transform.position + offset;
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }

    }

    
}
