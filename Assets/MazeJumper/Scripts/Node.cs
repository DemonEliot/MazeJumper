using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private int nodeKey;
    private GameObject nodeUp, nodeDown, nodeLeft, nodeRight;
    private List<GameObject> nodesSentHereList;
    private GameObject environmentGameObject;
    private Vector3 positionToCheck;

    // Start is called before the first frame update
    private void Start()
    {
        environmentGameObject = this.transform.parent.gameObject;

        // Need to determine what type of node this is as to where the player could go from it
        switch (this.gameObject.tag)
        {
            case "start":
            case "floor":
            case "gate":
                // Can potentially walk in any direction, need to loop through all cubes and check if their positions are next to this node

                // This loop checks the 'environment' gameobject children
                foreach (Transform child in environmentGameObject.transform)
                {
                    if (child.position == this.transform.position + Vector3.forward)
                    {
                        nodeUp = child.gameObject;
                        child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
                    }

                    else if (child.position == this.transform.position + Vector3.back)
                    {
                        nodeDown = child.gameObject;
                        child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
                    }

                    else if (child.position == this.transform.position + Vector3.left)
                    {
                        nodeLeft = child.gameObject;
                        child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
                    }

                    else if (child.position == this.transform.position + Vector3.right)
                    {
                        nodeRight = child.gameObject;
                        child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
                    }
                }
                break;

            case "up":
                positionToCheck = this.transform.position;
                do
                {
                    positionToCheck += Vector3.forward;
                    foreach (Transform child in environmentGameObject.transform)
                    {
                        if (child.position == positionToCheck)
                        {
                            nodeUp = child.gameObject;
                            child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
                        }
                    }
                } while (nodeUp == null);
                break;

            case "down":
                positionToCheck = this.transform.position;
                do
                {
                    positionToCheck += Vector3.back;
                    foreach (Transform child in environmentGameObject.transform)
                    {
                        if (child.position == positionToCheck)
                        {
                            nodeDown = child.gameObject;
                            child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
                        }
                    }
                } while (nodeDown == null);
                break;

            case "left":
                positionToCheck = this.transform.position;
                do
                {
                    positionToCheck += Vector3.left;
                    foreach (Transform child in environmentGameObject.transform)
                    {
                        if (child.position == positionToCheck)
                        {
                            nodeLeft = child.gameObject;
                            child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
                        }
                    }
                } while (nodeLeft == null);
                break;

            case "right":
                positionToCheck = this.transform.position;
                do
                {
                    positionToCheck += Vector3.right;
                    foreach (Transform child in environmentGameObject.transform)
                    {
                        if (child.position == positionToCheck)
                        {
                            nodeRight = child.gameObject;
                            child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
                        }
                    }
                } while (nodeRight == null);
                break;
        }
    }

    public void GiveKeyToNode(int key)
    {
        nodeKey = key;
    }

    public int GetNodeKey()
    {
        return nodeKey;
    }

    public void AddNodeSentHere(GameObject node)
    {
        nodesSentHereList.Add(node);
    }
}
