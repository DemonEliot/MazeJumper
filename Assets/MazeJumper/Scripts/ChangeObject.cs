using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeObject : MonoBehaviour
{

    public int cubeIndex = 0;
    public GameObject[] cubeList = new GameObject[7];
    public bool oneTimeCreation = false;
    private Vector3 currentPos;

    private GameObject environment;

    private void GridSnap()
    {
        // This snaps to a grid when moving the cubes
        currentPos = transform.position;
        transform.position = new Vector3(Mathf.Round(currentPos.x), 0, Mathf.Round(currentPos.z));
    }

    private void ChangeCube()
    {
        // Creates new cube in location of current cube and destroys current cube.
        for (int i = 0; i < 7; i++)
        {
            if (i == cubeIndex)
            {

                if (environment != null)
                {
                    Instantiate(cubeList[i], gameObject.transform.position, Quaternion.identity, environment.transform);
                }
                else
                {
                    Instantiate(cubeList[i], gameObject.transform.position, Quaternion.identity);
                }

                oneTimeCreation = true;

                if (transform.parent == null || transform.parent == environment.transform)
                {
                    DestroyImmediate(this.gameObject);
                }
                else
                {
                    DestroyImmediate(transform.parent.gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Application.isEditor)
        {

            if (transform.parent == null || transform.parent.gameObject.name == Tags.ENVIRONMENT)
            {
                GridSnap();
            }

            // For ease of use, I added this script to both the cubes, and the components inside the cubes. (For ease of level editing, you can click on the top layer and change it from there...)
            // Change the number in the public editor and tick oneTimeCreation to change the cube.
            if (oneTimeCreation == false)
            {
                ChangeCube();
            }
        }
    }
}
