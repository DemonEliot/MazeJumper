using UnityEngine;
using System.Collections;

public class BreadcrumbMode : MonoBehaviour {

    public bool breadcrumbs = false;
    public ParticleSystem ps;
    int counter = 0;
    float time = 0;

    // Use this for initialization
    void Start () {
        ps = GetComponent<CharAnim>().particle;
    }
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!breadcrumbs && counter < 3)
            {
                ps = GetComponent<CharAnim>().particle;
                ps.startLifetime = 1000f;
                breadcrumbs = !breadcrumbs;
                counter++;
                time = Time.time + 5;
            }        
        }
        if (breadcrumbs && time < Time.time)
        {
            breadcrumbs = !breadcrumbs;
            ps = GetComponent<CharAnim>().particle;
            ps.startLifetime = 1;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ps.Clear();
            counter = 0;
        }
    }
}
