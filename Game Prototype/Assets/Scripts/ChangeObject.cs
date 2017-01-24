using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeObject : MonoBehaviour {

    public int obj = 0;
    public GameObject[] list = new GameObject[7];
    public bool oneTimeCreation = false;

    // Use this for initialization
    void Start ()
    {
        if (!Application.isPlaying && oneTimeCreation == false)
        {
            for (int i = 0; i < 7; i++)
            {
                if (i == obj)
                {
                    Instantiate(list[i],gameObject.transform.position,Quaternion.identity);
                    DestroyImmediate(this.gameObject);
                }
            }
            oneTimeCreation = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        

    }
}
