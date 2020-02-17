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

    private const string startTag = "start";
    private const string endTag = "end";
    private const string floorTag = "floor";
    private const string gateTag = "gate";
    private const string upTag = "up";
    private const string downTag = "down";
    private const string leftTag = "left";
    private const string rightTag = "right";

    // Start is called before the first frame update
    private void Start()
    {
        if (AllNodes.DoesDictionaryContainKey(this.transform.position))
        {
            Debug.Log("WARNING! Multiple nodes exist at space: " + this.transform.position);
        }

        AllNodes.AddNodeToDictionary(this.gameObject);
        environment = this.transform.parent.gameObject;

        // Need to determine what type of node this is as to where the player could go from it
        switch (this.gameObject.tag)
        {
            case endTag:
                break;
            case startTag:
            case floorTag:
            case gateTag:
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

            case upTag:
            case downTag:
            case leftTag:
            case rightTag:
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
        List<GameObject> savedNodeToChild = new List<GameObject>();

        switch (this.gameObject.tag)
        {
            case upTag:
                directionToMove = Vector3.forward;
                savedNodeToChild.Add(nodeUp);
                break;
            case downTag:
                directionToMove = Vector3.back;
                savedNodeToChild.Add(nodeDown);
                break;
            case leftTag:
                directionToMove = Vector3.left;
                savedNodeToChild.Add(nodeLeft);
                break;
            case rightTag:
                directionToMove = Vector3.right;
                savedNodeToChild.Add(nodeRight);
                break;
        }

        do
        {
            positionToCheck += directionToMove;
            foreach (Transform child in environment.transform)
            {
                if (child.gameObject.tag != startTag || child.gameObject.tag != floorTag || child.gameObject.tag != endTag)
                {
                    if (child.position == positionToCheck)
                    {
                        // TODO Check if this actually works in C# ... Do I even need to use a list? Can I have a temp variable that points to the acutal Node variable?
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
