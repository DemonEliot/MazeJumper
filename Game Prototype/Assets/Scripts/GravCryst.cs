using UnityEngine;
using System.Collections;

public class GravCryst : MonoBehaviour {

    public GameObject canvas;
    public GameObject player;

	void OnTriggerEnter(Collider grav)
    {
        if (grav.gameObject.tag == "Player" && grav.gameObject.GetComponent<CharAnim>().intangible == false)
        {
            canvas.SetActive(true);
            player.GetComponent<BreadcrumbMode>().ps.Clear();
            Time.timeScale = 0;        
        }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * 16);
	}
}
