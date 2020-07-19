using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnap : MonoBehaviour
{
    private Vector3 currentPos;

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {

            if (transform.parent == null || transform.parent.gameObject.name == Tags.ENVIRONMENT)
            {
                Snap();
            }
        }
    }

    private void Snap()
    {
        // This snaps to a grid when moving the cubes
        currentPos = transform.position;
        transform.position = new Vector3(Mathf.Round(currentPos.x), 0, Mathf.Round(currentPos.z));
    }
}
