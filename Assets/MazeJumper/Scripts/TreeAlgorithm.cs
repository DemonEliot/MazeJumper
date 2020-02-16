using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAlgorithm : MonoBehaviour {

    // Just started trying to design a possible way of adding the cubes to an array and using a tree/node based way of navigating the maze.

    private GameObject[] startList;
    private GameObject startNode;
    private List<GameObject> nodeList;
    private int keyIndex = 0;

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

        startNode = startList[0];

        AddNodeToList(startNode);
	}

    void AddNodeToList(GameObject node)
    {
        node.GetComponent<Node>().GiveKeyToNode(keyIndex);
        keyIndex++;
        nodeList.Add(node);
    }

    // Need to recusively (starting from startCube) go through each cube
}
