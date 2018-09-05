using UnityEngine;
using System.Collections;

public class CubeFunctions : MonoBehaviour {

    Vector3 direction;
    Vector3 targetPos;

    public ParticleSystem.EmissionModule portalEmmod;
    public GameObject endOfLevelCanvas;

    // Was trying to get the portals to appear and disappear, but it doesn't work yet. 

    //void EnablePortal(Collider gridDetect)
    //{
    //    portalParticle = gridDetect.gameObject.GetComponentInChildren<ParticleSystem>();
    //    portalEmmod = portalParticle.emission;
    //    portalEmmod.enabled = true;
    //}

    //void DisablePortal()
    //{
    //    portalEmmod.enabled = false;
    //}

    void OnTriggerEnter(Collider gridDetect)
    {
        //Detects if the player collides with a "portal" and then moves them in the correct direction.
        if (gridDetect.gameObject.tag == "right")
        {
            PortalMovement(Vector3.forward, gridDetect);
            direction = Vector3.forward;
        }

        else if (gridDetect.gameObject.tag == "down")
        {
            PortalMovement(Vector3.right, gridDetect);
            direction = Vector3.right;
        }

        else if (gridDetect.gameObject.tag == "left")
        {
            //EnablePortal(gridDetect);
            PortalMovement(Vector3.back, gridDetect);
            direction = Vector3.back;
            //DisablePortal();
        }

        else if (gridDetect.gameObject.tag == "up")
        {
            PortalMovement(Vector3.left, gridDetect);
            direction = Vector3.left;
        }

        //Colliding with a gate makes the player "reappear".
        else if (gridDetect.gameObject.tag == "gate")
        {
            GetComponent<CharAnim>().intangible = false;
        }       
        
        //will spawn the level end canvas.
        else if (gridDetect.gameObject.tag == "crystal" && gameObject.GetComponent<CharAnim>().intangible == false)
        {
            endOfLevelCanvas.SetActive(true);
            //TODO redo breadcrumb mode
            //gameObject.GetComponent<BreadcrumbMode>().ps.Clear();

            //TODO make a less messy end of level
            Time.timeScale = 0;
        }

    }

    void PortalMovement(Vector3 direction, Collider portal)
    {
        //Makes the player "intangible", moves them one square in the direction the portal faces.
        GetComponent<CharAnim>().intangible = true;
        targetPos = portal.gameObject.transform.position + direction + new Vector3(0,0.5f,0);
        //Have to update the player position, otherwise it will try to move back to the sqaure it was on.
        GetComponent<CharAnim>().pos = targetPos;
    }

    void Update() {

        //If the player is still intangible after moving one square, then they need to move again.
        if (GetComponent<CharAnim>().intangible == true)
        {
            if (transform.position.x > targetPos.x-0.1 && transform.position.x < targetPos.x + 0.1 && 
                transform.position.z > targetPos.z - 0.1 && transform.position.z < targetPos.z + 0.1)
            {
                transform.position = targetPos;
                targetPos = GetComponent<CharAnim>().pos = transform.position + direction;
                GetComponent<CharAnim>().pos = transform.position + direction;
               
            }
            gameObject.transform.forward = direction;
        }
    }
}