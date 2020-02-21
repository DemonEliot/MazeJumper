using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllNodes
{

    private static Dictionary<Vector2, GameObject> allNodes = new Dictionary<Vector2, GameObject>();

    public static bool DoesDictionaryContainKey(Vector2 key)
    {
        return allNodes.ContainsKey(key);
    }

    public static GameObject GetNodeByPosition(Vector2 position)
    {
        return allNodes[position];
    }

    public static void AddNodeToDictionary(GameObject newNode)
    {
        allNodes.Add(Vector3Extension.AsVector2(newNode.transform.position), newNode);
    }

    public static void ClearAllNodes()
    {
        allNodes.Clear();
    }

}
