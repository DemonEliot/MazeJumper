using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAlgorithm : MonoBehaviour {

    // Just started trying to design a possible way of adding the cubes to an array and using a tree/node based way of navigating the maze.

    private GameObject[] startList;
    private GameObject startNode;

    private static Dictionary<Vector3, GameObject> allNodes = new Dictionary<Vector3, GameObject>();

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
	}

  public GameObject GetNodeByPosition(Vector3 position) {
      return allNodes[position];
  }

  public void AddNodeToDictionary(GameObject newNode) {
      allNodes.Add(newNode.transform.position, newNode);
  }

}
