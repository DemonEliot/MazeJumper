using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeObject : MonoBehaviour
{

    /*
     * This class is currently getting weird errors, and I won't be using it too much anyway, so I've locked everything down for now
     */

    /*
    public int cubeIndex = 0;
    public GameObject[] cubeList = new GameObject[7];
    public bool oneTimeCreation = false;
    private GameObject environment;
    private bool checkForEnvironment = false;
    */

    /*
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

                oneTimeCreation = false;

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
    */

    private void OnEnable()
    {
        if (Application.isEditor)
        {

            // For ease of use, I added this script to both the cubes, and the components inside the cubes. (For ease of level editing, you can click on the top layer and change it from there...)
            // Change the number in the public editor and tick oneTimeCreation to change the cube.
            
            /*
            if (oneTimeCreation == true)
            {
                if (!checkForEnvironment)
                {
                    if (transform.parent.gameObject.name == Tags.ENVIRONMENT)
                    {
                        environment = transform.parent.gameObject;
                    }
                }

                ChangeCube();
            }
            */

        }
    }
}
