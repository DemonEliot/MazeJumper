using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNodes : MonoBehaviour {

    private GameObject[] startList;
    private GameObject startCube;

	// Use this for initialization
	void Start ()
    {
        startList = GameObject.FindGameObjectsWithTag("start");

        if (startList == null)
        {
            Debug.LogError("Missing a start location");
        }

        else if (startList.Length >= 2)
        {
            Debug.LogError("Have multiple start locations");
        }

        startCube = startList[0];

        CreateTree();
	}

    void CreateTree ()
    {

        //startCube.GetComponent<ChangeObject>().up
    }

}
