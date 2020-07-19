using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private GameObject nodeUp, nodeDown, nodeLeft, nodeRight;
    private List<GameObject> nodesGoesHereList = new List<GameObject>();
    private GameObject environment;

    void Awake()
    {
        if (AllNodes.DoesDictionaryContainKey(Vector3Extension.AsVector2(this.transform.position)))
        {
            Debug.Log("WARNING! Multiple nodes exist at space: " + this.transform.position);
        }

        else { 

        AllNodes.AddNodeToDictionary(this.gameObject);
        environment = this.transform.parent.gameObject;

            // Need to determine what type of node this is as to where the player could go from it
            switch (this.gameObject.tag)
            {
                case Tags.FLOOR:
                case Tags.GATE:

                    // Can potentially walk in any direction, need to loop through all cubes and check if their positions are next to this node
                    // This loop checks the 'environment' gameobject children
                    foreach (Transform child in environment.transform)
                    {
                        if (child.position == this.transform.position + Vector3.forward)
                        {
                            nodeUp = child.gameObject;
                            child.gameObject.GetComponent<Node>().AddNodeGoesHere(this.gameObject);
                        }

                        else if (child.position == this.transform.position + Vector3.back)
                        {
                            nodeDown = child.gameObject;
                            child.gameObject.GetComponent<Node>().AddNodeGoesHere(this.gameObject);
                        }

                        else if (child.position == this.transform.position + Vector3.left)
                        {
                            nodeLeft = child.gameObject;
                            child.gameObject.GetComponent<Node>().AddNodeGoesHere(this.gameObject);
                        }

                        else if (child.position == this.transform.position + Vector3.right)
                        {
                            nodeRight = child.gameObject;
                            child.gameObject.GetComponent<Node>().AddNodeGoesHere(this.gameObject);
                        }
                    }
                    break;

                case Tags.UP:
                case Tags.DOWN:
                case Tags.LEFT:
                case Tags.RIGHT:
                    GetNextNodeFromPortalMovement();
                    break;
            }
        }
    }

    private void GetNextNodeFromPortalMovement()
    {
        Vector3 positionToCheck = this.transform.position;
        Vector3 directionToMove = new Vector3();

        switch (this.gameObject.tag)
        {
            case Tags.UP:
                directionToMove = Vector3.forward;
                break;
            case Tags.DOWN:
                directionToMove = Vector3.back;
                break;
            case Tags.LEFT:
                directionToMove = Vector3.left;
                break;
            case Tags.RIGHT:
                directionToMove = Vector3.right;
                break;
        }

        bool checkForNode = true;
        int safetyCounter = 0;

        do
        {
            positionToCheck += directionToMove;
            foreach (Transform child in environment.transform)
            {
                if (child.gameObject.tag != Tags.START && child.gameObject.tag != Tags.FLOOR && child.gameObject.tag != Tags.END)
                {
                    if (child.position == positionToCheck)
                    {
                        switch (this.gameObject.tag)
                        {
                            case Tags.UP:
                                nodeUp = child.gameObject;
                                break;
                            case Tags.DOWN:
                                nodeDown = child.gameObject;
                                break;
                            case Tags.LEFT:
                                nodeLeft = child.gameObject;
                                break;
                            case Tags.RIGHT:
                                nodeRight = child.gameObject;
                                break;
                        }
                        child.gameObject.GetComponent<Node>().AddNodeGoesHere(this.gameObject);
                        checkForNode = false;
                    }
                }
            }
            safetyCounter++;
            if (safetyCounter > 50)
            {
                Debug.LogError("ERROR with map design: Node " + this.tag + " at " + this.transform.position);
                checkForNode = false;
            }
        } while (checkForNode);
    }

    public void AddNodeGoesHere(GameObject node)
    {
        nodesGoesHereList.Add(node);
    }

    public GameObject GetNodeUp()
    {
        return nodeUp;
    }

    public GameObject GetNodeDown()
    {
        return nodeDown;
    }

    public GameObject GetNodeLeft()
    {
        return nodeLeft;
    }

    public GameObject GetNodeRight()
    {
        return nodeRight;
    }
}
