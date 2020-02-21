using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{

    public GameObject cross;
    private GameObject instantiatedCross;
    private bool isCrossed = false;

    private void OnMouseUpAsButton()
    {
        // When clicking on a block (this script is attached to all the cubes), either create a cross on that cube, or destroy the cross.
        if (!isCrossed)
        {
            instantiatedCross = (GameObject)Instantiate(cross, new Vector3(transform.position.x, 0.6f, transform.position.z), Quaternion.Euler(90, 0, 0));
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
}
