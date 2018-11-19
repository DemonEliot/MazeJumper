using UnityEngine;
using System.Collections;

public class CharAnim : MonoBehaviour {

    private Animator animComp;
    private float animState;
    private bool isWalking;
    public Vector3 pos;
    private Vector3 startPos;
    public Quaternion startRot;
    private float speed = 2;
    private float time = 0;
    public bool intangible = false;
    public bool playerMove = true;

    // Variables for particle use
    public SkinnedMeshRenderer[] listOfMeshRender;
    public ParticleSystem particle;
    public ParticleSystem.EmissionModule emmod;

    // Sounds
    //public AudioClip portalSound;
    //private AudioSource source;

    private GameObject mainCamera;

    // Use this for initialization
    void Start () {

        animComp = this.GetComponent<Animator>();
        pos = transform.position;
        startPos = transform.position;
        startRot = transform.rotation;
        listOfMeshRender = GetComponentsInChildren<SkinnedMeshRenderer>();
        particle = GetComponentInChildren<ParticleSystem>();
        emmod = particle.emission;
        mainCamera = GameObject.FindWithTag("MainCamera");

    }

	// Update is called once per frame
	void Update ()
    {
        //Before any movement, a ray is used to detect if the player can move there.
        if (Input.GetKey (KeyCode.UpArrow) && time < Time.time && playerMove)
        {
            //Up
            Ray(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.RightArrow) && time < Time.time && playerMove)
        {
            //Right
            Ray(Vector3.right);
        }
        
        else if (Input.GetKey(KeyCode.DownArrow) && time < Time.time && playerMove)
        {
            //Down
            Ray(Vector3.back);
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && time < Time.time && playerMove)
        {
            //Left
            Ray(Vector3.left);
        }

        //If the player is walking and they've finished moving, then make them stop walking.
        if (isWalking == true && time < Time.time)
        {
            isWalking = false;
            animState = 0;
            animComp.SetFloat("Speed", animState);
        }

        // If the player is not teleporting through portals...
        if (!intangible)
        {
            //source.Stop();
            speed = 2;
            if (transform.position.y < 0.49)
            {
                transform.position = new Vector3(transform.position.x, 0.49f, transform.position.z);
            }
            foreach (SkinnedMeshRenderer render in listOfMeshRender)
            {
                render.enabled = true;
            }   
            emmod.enabled = false;
          
        }
        else if (intangible)
        {
            //source.PlayOneShot(portalSound,0.1f);
            speed = 4;
            foreach (SkinnedMeshRenderer render in listOfMeshRender)
            {
                render.enabled = false;
            }
            emmod.enabled = true;
        }
        //Keeps the player to a grid based movement.
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    public void Movement(string direction)
    {
        switch (direction)
        {
            case ("Up"):
                Ray(Vector3.forward);
                break;
            case ("Down"):
                Ray(Vector3.back);
                break;
            case ("Right"):
                Ray(Vector3.right);
                break;
            case ("Left"):
                Ray(Vector3.left);
                break;
            default:
                break;
        }
    }

    public void StopMovement()
    {
        isWalking = false;
        animState = 0;
        animComp.SetFloat("Speed", animState);
    }

    void Rotate(Vector3 facing)
    {
        //The player faces the direction they move in.
        transform.forward = facing;
    }

    void Move(Vector3 moving)
    {
        //TODO change so input is ignored when moving without using time
        //The player moves one co-ordinate at a time.
        pos += moving;
        isWalking = true;
        animState = 1;
        animComp.SetFloat("Speed", animState);
        time = Time.time + 0.3f;
       
    }

    void Ray(Vector3 rayCheck)
    {
        
        if (intangible == false)
        {
            Ray emptyCheck = new Ray(transform.position, rayCheck);

            Debug.DrawRay(transform.position, rayCheck * 1f, Color.red);
            //For everything that the raycast hits, starting from the player and going 1 square...
            foreach (RaycastHit hit in Physics.RaycastAll(emptyCheck, 1f))
            {
                //if it hits something, not including the player or floor they're standing on...
                //TODO change so uses layer mask instead
                if (hit.transform != transform && hit.transform.position != transform.position)
                {
                    Rotate(rayCheck);
                    Move(rayCheck);
                }
            }
        }
    }

    public void CharReset(GameObject environment)
    {
        StopMovement();
        transform.position = startPos;
        pos = startPos;
        transform.rotation = startRot;
        intangible = false;
        playerMove = true;
        particle.Clear();

        if (mainCamera.GetComponent<CameraMove>().camMove)
        {
            mainCamera.GetComponent<CameraMove>().CamMode();
        }

        foreach (Transform child in environment.transform)
        {
            child.GetComponent<ChangeObject>().ResetCrosses();
        }
    }
}
