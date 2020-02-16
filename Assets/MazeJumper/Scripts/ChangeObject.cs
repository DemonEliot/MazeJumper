using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeObject : MonoBehaviour {

    public int cubeIndex = 0;
    public GameObject[] cubeList = new GameObject[7];
    public bool oneTimeCreation = false;
    private Vector3 currentPos;

    public GameObject cross;
    private GameObject instantiatedCross;
    private bool isCrossed = false;


    // These game objects will be used for the tree. These are nodes for what is in that direction.
    // Just going to comment it out for the moment... (Because of pushing this to Master Branch)
    // public GameObject up, right, down, left;

    private void OnMouseUpAsButton()
    {
        // When clicking on a block (this script is attached to all the cubes), either create a cross on that cube, or destroy the cross.
        if (!isCrossed)
        {
            instantiatedCross = (GameObject) Instantiate(cross, new Vector3(transform.position.x, 0.6f, transform.position.z), Quaternion.Euler(90, 0, 0));
            isCrossed = !isCrossed;
        }
        else
        {
            Destroy(instantiatedCross);
            isCrossed = !isCrossed;
        }

    }

    public void ResetCrosses()
    {
        if (isCrossed)
        {
            Destroy(instantiatedCross);
            isCrossed = !isCrossed;
        }
    }

	// Update is called once per frame
	private void Update ()
    {
        if (Application.isEditor)
        {
            // This snaps to a grid when moving the cubes, but only if it is the parent gameobject.
            if (transform.parent == null)
            {
                currentPos = transform.position;
                transform.position = new Vector3(Mathf.Round(currentPos.x), 0, Mathf.Round(currentPos.z));
            }

            // For ease of use, I added this script to both the cubes, and the components inside the cubes. (For ease of level editing, you can click on the top layer and change it from there...)
            // Change the number in the public editor and tick oneTimeCreation to change the cube.
            if (oneTimeCreation == false)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i == cubeIndex)
                    {
                        // Creates new cube in location of current cube and destroys current cube.
                        Instantiate(cubeList[i], gameObject.transform.position, Quaternion.identity);
                        oneTimeCreation = true;

                        if (transform.parent == null)
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
        }
    }
}
