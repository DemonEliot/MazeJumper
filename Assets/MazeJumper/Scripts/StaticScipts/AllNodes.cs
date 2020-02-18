using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllNodes
{

    private static Dictionary<Vector2, GameObject> allNodes = new Dictionary<Vector2, GameObject>();

    public static const string START = "start";
    public static const string END = "end";
    public static const string FLOOR = "floor";
    public static const string GATE = "gate";
    public static const string UP = "up";
    public static const string DOWN = "down";
    public static const string LEFT = "left";
    public static const string RIGHT = "right";

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

}
