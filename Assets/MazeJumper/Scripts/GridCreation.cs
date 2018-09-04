using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GridCreation : MonoBehaviour {

    // Just define how many columns and rows in the public editor, and then drag the gameobject into the scene to instantly create the grid.

    public int gridColumn;
    public int gridRow;
    public GameObject cube;

    // Use this for initialization
    void Start ()
    {
        if (Application.isEditor)
        {

            GameObject[,] grid = new GameObject[gridRow, gridColumn];

            for (int i = 0; i < gridRow; i++)
            {
                for (int j = 0; j < gridColumn; j++)
                {
                    grid[i, j] = (GameObject)Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
        }
    }
	
}
