using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SolveMaze
{
    public static void RunMazeSolver()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        GameObject currentNode = player.GetComponent<CharacterManager>().GetCurrentNodeObject();

        Debug.Log("Map size is: " + currentNode.transform.parent.transform.childCount);
    }
}
