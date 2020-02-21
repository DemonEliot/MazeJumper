using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeChange : MonoBehaviour {

    // This script is attached to an empty gameobject called GameChange.
    // It is used to update all cubes in the scene to the latest prefab (because the cubes are clones, they are no longer attached to their prefabs...)

    GameObject[] rightCubeList, leftCubeList, upCubeList, downCubeList, gateCubeList, floorCubeList, startCubeList, endCubeList;
    public GameObject[] newCubeList = new GameObject[6];
    private GameObject environment;

    // Use this for initialization
    void Start()
    {
        if (Application.isEditor)
        {
            floorCubeList = GameObject.FindGameObjectsWithTag(Tags.FLOOR);
            gateCubeList = GameObject.FindGameObjectsWithTag(Tags.GATE);
            upCubeList = GameObject.FindGameObjectsWithTag(Tags.UP);
            downCubeList = GameObject.FindGameObjectsWithTag(Tags.DOWN);
            leftCubeList = GameObject.FindGameObjectsWithTag(Tags.LEFT);
            rightCubeList = GameObject.FindGameObjectsWithTag(Tags.RIGHT);
            startCubeList = GameObject.FindGameObjectsWithTag(Tags.START);
            endCubeList = GameObject.FindGameObjectsWithTag(Tags.END);

            environment = GameObject.Find(Tags.ENVIRONMENT);
            if (environment == null)
            {
                //environment = new GameObject(Tags.ENVIRONMENT);
                Instantiate(environment, new Vector3(0, 0, 0), Quaternion.identity);
            }

            InstantiateNewCubes(floorCubeList, 0);
            InstantiateNewCubes(gateCubeList, 1);
            InstantiateNewCubes(upCubeList, 2);
            InstantiateNewCubes(downCubeList, 3);
            InstantiateNewCubes(leftCubeList, 4);
            InstantiateNewCubes(rightCubeList, 5);
            InstantiateNewCubes(startCubeList, 0);
            InstantiateNewCubes(endCubeList, 0);
        }
    }

    private void InstantiateNewCubes(GameObject[] cubes, int cubeListIndex)
    {
        for (int i = cubes.Length - 1; i > -1; i--)
        {
            //Debug.Log(cubes[i]);
            if (environment != null)
            {
                Instantiate(newCubeList[cubeListIndex], cubes[i].transform.position, Quaternion.identity, environment.transform);
            }
            else
            {
                Instantiate(newCubeList[0], cubes[i].transform.position, Quaternion.identity);
            }
            DestroyImmediate(cubes[i]);
        }
    }
}
