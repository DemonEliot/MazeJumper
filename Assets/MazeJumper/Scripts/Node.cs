using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private int nodeKey;
    private GameObject nodeUp, nodeDown, nodeLeft, nodeRight;
    private List<GameObject> nodesSentHereList;
    private GameObject environmentGameObject;

    private readonly string startTag = "start";
    // TODO Add 'end' tag in Unity
    // TODO Reformat line indentations
    private readonly string endTag = "end";
    private readonly string floorTag = "floor";
    private readonly string gateTag = "gate";
    private readonly string upTag = "up";
    private readonly string downTag = "down";
    private readonly string leftTag = "left";
    private readonly string rightTag = "right";

    // Start is called before the first frame update
    private void Start()
    {
        environmentGameObject = this.transform.parent.gameObject;

        // Need to determine what type of node this is as to where the player could go from it
        switch (this.gameObject.tag)
        {
            case endTag:
                // TODO Win the game
                break;
            case startTag:
            case floorTag:
            case gateTag:
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

            case upTag:
            case downTag:
            case leftTag:
            case rightTag:
                getNextNodeFromPortalMovement();
                break;
            case default:
                Debug.Log("This gameobject has an unexpected tag of: " + this.gameObject.tag );
                break;
        }
    }

    private void getNextNodeFromPortalMovement()
    {
      Vector3 positionToCheck = this.transform.position;
      Vector3 directionToMove;
      List<GameObject> savedNodeToChild;

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
          foreach (Transform child in environmentGameObject.transform)
          {
            if (child.gameObject.tag != startTag || child.gameObject.tag != floorTag || child.gameObject.tag != endTag)
            {
              if (child.position == positionToCheck)
              {
                  // TODO Check if this actually works in C# ... Do I even need to use a list? Can I have a temp variable that points to the acutal Node variable?
                  savedNodeToChild.get(0) = child.gameObject;
                  child.gameObject.GetComponent<Node>().AddNodeSentHere(this.gameObject);
              }
            }
          }
      } while (savedNodeToChild.get(0) == null);
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
