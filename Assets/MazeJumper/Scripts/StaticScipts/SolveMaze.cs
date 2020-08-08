using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SolveMaze
{
    private static List<GameObject> checkedNodes;
    private static List<List<GameObject>> solvedPaths;
    private static List<int> gateCountList;
    private static int gateCounter = 0;
    private static readonly string[] NODES = { "portal", "up", "down", "left", "right" };
    private const string NODEPORTAL = "portal";
    private const string NODEUP = "up";
    private const string NODEDOWN = "down";
    private const string NODELEFT = "left";
    private const string NODERIGHT = "right";


    public static void RunMazeSolver()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        GameObject currentNode = player.GetComponent<CharacterManager>().GetCurrentNodeObject();
        checkedNodes = new List<GameObject>();
        solvedPaths = new List<List<GameObject>>();
        gateCountList = new List<int>();

        CheckNodes(currentNode);

        Debug.Log("Map size is: " + currentNode.transform.parent.transform.childCount);

        if (solvedPaths.Count > 1)
        {
            for (int i = solvedPaths.Count - 2; i >= 0; i--)
            {
                if (solvedPaths[i].Count > solvedPaths[i + 1].Count)
                {
                    solvedPaths.RemoveAt(i);
                    gateCountList.RemoveAt(i);
                }
                else
                {
                    solvedPaths.RemoveAt(i + 1);
                    gateCountList.RemoveAt(i + 1);
                }
            }
        }

        Debug.Log("Gate Count: " + gateCountList[0]);

        foreach (GameObject node in solvedPaths[0])
        {
            foreach (Transform child in node.GetComponentInChildren<Transform>())
                if (child.gameObject.name == "Top")
                {
                    child.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                }
        }

        if (currentNode.tag == Tags.GATE)
        {
            MazeDifficulty.CalculateMazeDifficulty(currentNode.transform.parent.transform.childCount, gateCountList[0]-1);
        }
        else
        {
            MazeDifficulty.CalculateMazeDifficulty(currentNode.transform.parent.transform.childCount, gateCountList[0]);
        }
    }

    private static void CheckNodes(GameObject currentNode)
    {
        checkedNodes.Add(currentNode);
        Node currentNodeScript = currentNode.GetComponent<Node>();

        if (currentNode.tag.Equals(Tags.END))
        {
            List<GameObject> path = new List<GameObject>(checkedNodes);
            solvedPaths.Add(path);

            int currentGateCount = gateCounter;
            gateCountList.Add(currentGateCount);

            checkedNodes.Remove(currentNode);
            return;
        }

        if (currentNode.tag.Equals(Tags.GATE))
        {
            gateCounter++;
        }

        CheckNextNode(currentNodeScript, currentNodeScript.GetNodeGoesHere());
        CheckNextNode(currentNodeScript, currentNodeScript.GetNodeUp());
        CheckNextNode(currentNodeScript, currentNodeScript.GetNodeDown());
        CheckNextNode(currentNodeScript, currentNodeScript.GetNodeLeft());
        CheckNextNode(currentNodeScript, currentNodeScript.GetNodeRight());

        if (currentNode.tag.Equals(Tags.GATE))
        {
            gateCounter--;
        }
        checkedNodes.Remove(currentNode);
    }

    private static void CheckNextNode(Node currentNodeScript, GameObject nodeToCheck)
    {
        bool nextChecked = false;
        if (nodeToCheck != null)
        {
            foreach (GameObject node in checkedNodes)
            {
                if (nodeToCheck.Equals(node) && !nodeToCheck.tag.Equals(Tags.END))
                {
                    nextChecked = true;
                    break;
                }
            }
            foreach (List<GameObject> solution in solvedPaths)
            {
                int counter = 0;
                foreach(GameObject node in solution)
                {
                    if (nodeToCheck.Equals(node))
                    {
                        if (checkedNodes.Count < counter)
                        {
                            break;
                        }
                        nextChecked = true;
                        break;
                    }
                    counter++;
                }
            }
            if (!nextChecked)
            {
                CheckNodes(nodeToCheck);
            }
        }
    }
}
