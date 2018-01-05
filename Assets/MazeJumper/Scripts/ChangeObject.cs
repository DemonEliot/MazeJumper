using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeObject : MonoBehaviour {

    public int obj = 0;
    public GameObject[] list = new GameObject[7];
    public GameObject cross;
    private GameObject instantiatedCross;
    public bool oneTimeCreation = false;
    private bool crossed = false;

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
	
    void OnMouseDown()
    {
        Debug.Log("Block clicked");
        if (!crossed)
        {
            instantiatedCross = (GameObject) Instantiate(cross, new Vector3(transform.position.x, 0.6f, transform.position.z), Quaternion.Euler(90, 0, 0));
            crossed = !crossed;
        }
        else
        {
            Destroy(instantiatedCross);
            crossed = !crossed;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        
    }
}
