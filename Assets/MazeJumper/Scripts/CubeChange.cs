using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeChange : MonoBehaviour {

    // This script is attached to an empty gameobject called GameChange.
    // It is used to update all cubes in the scene to the latest prefab (because the cubes are clones, they are no longer attached to their prefabs...)

    GameObject[] rightCubeList, leftCubeList, upCubeList, downCubeList, gateCubeList, floorCubeList;
    public GameObject[] newCubeList = new GameObject[6];

    // Use this for initialization
    void Start()
    {
        if (Application.isEditor)
        {
            rightCubeList = GameObject.FindGameObjectsWithTag("right");
            leftCubeList = GameObject.FindGameObjectsWithTag("left");
            upCubeList = GameObject.FindGameObjectsWithTag("up");
            downCubeList = GameObject.FindGameObjectsWithTag("down");
            gateCubeList = GameObject.FindGameObjectsWithTag("gate");
            floorCubeList = GameObject.FindGameObjectsWithTag("floor");

            for (int i = rightCubeList.Length-1; i > -1; i--)
            {
                //Debug.Log(rightCubeList[i]);
                Instantiate(newCubeList[4], rightCubeList[i].transform.position, Quaternion.identity);
                DestroyImmediate(rightCubeList[i]);
            }

            for (int i = leftCubeList.Length-1; i > -1; i--)
            {
                //Debug.Log(leftCubeList[i]);
                Instantiate(newCubeList[3], leftCubeList[i].transform.position, Quaternion.identity);
                DestroyImmediate(leftCubeList[i]);
            }

            for (int i = downCubeList.Length-1; i > -1; i--)
            {
                //Debug.Log(downCubeList[i]);
                Instantiate(newCubeList[2], downCubeList[i].transform.position, Quaternion.identity);
                DestroyImmediate(downCubeList[i]);
            }

            for (int i = upCubeList.Length-1; i > -1; i--)
            {
                //Debug.Log(upCubeList[i]);
                Instantiate(newCubeList[5], upCubeList[i].transform.position, Quaternion.identity);
                DestroyImmediate(upCubeList[i]);
            }

            for (int i = gateCubeList.Length-1; i > -1; i--)
            {
                //Debug.Log(gateCubeList[i]);
                Instantiate(newCubeList[1], gateCubeList[i].transform.position, Quaternion.identity);
                DestroyImmediate(gateCubeList[i]);
            }

            for (int i = floorCubeList.Length - 1; i > -1; i--)
            {
                //Debug.Log(floorCubeList[i]);
                Instantiate(newCubeList[0], floorCubeList[i].transform.position, Quaternion.identity);
                DestroyImmediate(floorCubeList[i]);
            }
        }
    }
}
