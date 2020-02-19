using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private int nodeKey;
    private GameObject nodeUp, nodeDown, nodeLeft, nodeRight;
    private List<GameObject> nodesGoesHereList = new List<GameObject>();
    private GameObject environment;
    private GameObject eventSystem;

    // Start is called before the first frame update
    private void Start()
    {
        if (AllNodes.DoesDictionaryContainKey(Vector3Extension.AsVector2(this.transform.position)))
        {
            Debug.Log("WARNING! Multiple nodes exist at space: " + this.transform.position);
        }

        AllNodes.AddNodeToDictionary(this.gameObject);
        environment = this.transform.parent.gameObject;

        // Need to determine what type of node this is as to where the player could go from it
        switch (this.gameObject.tag)
        {
            case Tags.END:
                break;
            case Tags.START:
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
            default:
                Debug.Log("This gameobject has an unexpected tag of: " + this.gameObject.tag);
                break;
        }
    }

    private void GetNextNodeFromPortalMovement()
    {
        Vector3 positionToCheck = this.transform.position;
        Vector3 directionToMove = new Vector3();
        // Rename to savedChildToNode
        List<GameObject> savedNodeToChild = new List<GameObject>();

        switch (this.gameObject.tag)
        {
            case Tags.UP:
                directionToMove = Vector3.forward;
                savedNodeToChild.Add(nodeUp);
                break;
            case Tags.DOWN:
                directionToMove = Vector3.back;
                savedNodeToChild.Add(nodeDown);
                break;
            case Tags.LEFT:
                directionToMove = Vector3.left;
                savedNodeToChild.Add(nodeLeft);
                break;
            case Tags.RIGHT:
                directionToMove = Vector3.right;
                savedNodeToChild.Add(nodeRight);
                break;
        }

        do
        {
            positionToCheck += directionToMove;
            foreach (Transform child in environment.transform)
            {
                if (child.gameObject.tag != Tags.START || child.gameObject.tag != Tags.FLOOR || child.gameObject.tag != Tags.END)
                {
                    if (child.position == positionToCheck)
                    {
                        savedNodeToChild[0] = child.gameObject;
                        child.gameObject.GetComponent<Node>().AddNodeGoesHere(this.gameObject);
                    }
                }
            }
        } while (savedNodeToChild[0] == null);
    }

    public void GiveKeyToNode(int key)
    {
        nodeKey = key;
    }

    public int GetNodeKey()
    {
        return nodeKey;
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
