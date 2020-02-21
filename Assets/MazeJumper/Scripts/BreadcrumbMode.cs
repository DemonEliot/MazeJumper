//using UnityEngine;
//using System.Collections;

//public class BreadcrumbMode : MonoBehaviour {

//    // This feature isn't currently being used. Plans are to potentially change it to work better in the future.

//    public bool breadcrumbs = false;
//    public ParticleSystem ps;
//    int counter = 0;
//    float time = 0;

//    // Use this for initialization
//    void Start () {
//        ps = GetComponent<CharacterController>().GetPlayerParticle();
//    }
	
//	// Update is called once per frame
//	void Update () {
	
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            // If breadcrumbs currently isn't active and also you haven't used it 3 times already
//            if (!breadcrumbs && counter < 3)
//            {
//                // Get the particles, set the lifetime to A LOT, set breadcrumbs as active and increase the counter. Set the time for 5 seconds away
//                ps = GetComponent<CharacterController>().GetPlayerParticle();
//                ps.startLifetime = 1000f;
//                breadcrumbs = !breadcrumbs;
//                counter++;
//                time = Time.time + 5;
//            }        
//        }
//        // After 5 seconds of being active...
//        if (breadcrumbs && time < Time.time)
//        {
//            // Deactivate breadcrumbs mode and restore the particle lifetime to normal
//            breadcrumbs = !breadcrumbs;
//            ps = GetComponent<CharacterController>().GetPlayerParticle();
//            ps.startLifetime = 1;
//        }
//        if (Input.GetKeyDown(KeyCode.V))
//        {
//            // Clears away all the particles on screen and resets the counter
//            ps.Clear();
//            counter = 0;
//        }
//    }
//}
