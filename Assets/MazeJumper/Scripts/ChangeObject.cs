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
    private Vector3 currentPos;

    // Use this for initialization
    void Start ()
    {
        
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
        if (Application.isEditor)
        {
            if (transform.parent == null)
            {
                currentPos = transform.position;
                transform.position = new Vector3(Mathf.Round(currentPos.x), 0, Mathf.Round(currentPos.z));
            }

            if (oneTimeCreation == false)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i == obj)
                    {
                        Vector3 t = gameObject.transform.position;
                        t.y = 0;
                        Instantiate(list[i], t, Quaternion.identity);
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
