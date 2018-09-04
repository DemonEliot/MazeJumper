using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNodes : MonoBehaviour {

    // Just started trying to design a possible way of adding the cubes to an array and using a tree/node based way of navigating the maze.

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
