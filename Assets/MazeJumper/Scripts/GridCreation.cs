﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GridCreation : MonoBehaviour {

    public int gridColumn;
    public int gridRow;
    public GameObject cube;

    // Use this for initialization
    void Start ()
    {
        if (!Application.isPlaying)
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
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}
