using UnityEngine;
using System.Collections;

public class CubeFunctions : MonoBehaviour {

    Vector3 direction;
    Vector3 targetPos;

    public ParticleSystem portalParticle;
    public ParticleSystem.EmissionModule portalEmmod;

    void EnablePortal(Collider gridDetect)
    {
        portalParticle = gridDetect.gameObject.GetComponentInChildren<ParticleSystem>();
        portalEmmod = portalParticle.emission;
        portalEmmod.enabled = true;
    }

    void DisablePortal()
    {
        portalEmmod.enabled = false;
    }

    void OnTriggerEnter(Collider gridDetect)
    {
        //Detects if the player collides with a "portal" and then moves them in the correct direction.
        if (gridDetect.gameObject.tag == "right")
        {
            portalMovement(Vector3.forward, gridDetect);
            direction = Vector3.forward;
        }

        else if (gridDetect.gameObject.tag == "down")
        {
            portalMovement(Vector3.right, gridDetect);
            direction = Vector3.right;
        }

        else if (gridDetect.gameObject.tag == "left")
        {
            EnablePortal(gridDetect);
            portalMovement(Vector3.back, gridDetect);
            direction = Vector3.back;
            DisablePortal();
        }

        else if (gridDetect.gameObject.tag == "up")
        {
            portalMovement(Vector3.left, gridDetect);
            direction = Vector3.left;
        }

        //Colliding with a gate makes the player "reappear".
        else if (gridDetect.gameObject.tag == "gate")
        {
            GetComponent<CharAnim>().intangible = false;
        }

    }

    void portalMovement(Vector3 direction, Collider portal)
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