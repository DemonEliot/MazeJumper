using UnityEngine;
using System.Collections;

public class CharAnim : MonoBehaviour {

    private Animator animComp;
    private int animState;
    private bool isWalking;
    public Vector3 pos;
    float speed = 2;
    float time = 0;
    public bool intangible = false;
    public bool playerMove = true;

    public SkinnedMeshRenderer[] listOfMeshRender;
    public ParticleSystem particle;
    public ParticleSystem.EmissionModule emmod;

    public AudioClip portalSound;
    private AudioSource source;
    

    // Use this for initialization
    void Start () {

        animComp = this.GetComponent<Animator>();
        pos = transform.position;
        listOfMeshRender = GetComponentsInChildren<SkinnedMeshRenderer>();
        particle = GetComponentInChildren<ParticleSystem>();
        emmod = particle.emission;

        source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        //Before any movement, a ray is used to detect if the player can move there.
        if (Input.GetKey (KeyCode.RightArrow) && time < Time.time && playerMove)
        {
            ray(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.DownArrow) && time < Time.time && playerMove)
        {
            ray(Vector3.right);
        }
        
        else if (Input.GetKey(KeyCode.LeftArrow) && time < Time.time && playerMove)
        {
            ray(Vector3.back);
        }

        else if (Input.GetKey(KeyCode.UpArrow) && time < Time.time && playerMove)
        {
            ray(Vector3.left);
        }

        //If the player is walking and they've finished moving, then make them stop walking.
        if (isWalking == true && time < Time.time)
        {
            isWalking = false;
            animState = 0;
            animComp.SetInteger("JimState", animState);
        }

        // If the player is not teleporting through portals...
        if (!intangible)
        {
            source.Stop();
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
            source.PlayOneShot(portalSound,0.1f);
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

    void Rotate(Vector3 facing)
    {
        //The player faces the direction they move in.
        transform.forward = facing;
    }

    void Move(Vector3 moving)
    {

        //The player moves one co-ordinate at a time.
        pos += moving;
        isWalking = true;
        animState = 1;
        animComp.SetInteger("JimState", animState);
        time = Time.time + 0.3f;
       
    }

    void ray(Vector3 rayCheck)
    {
        if (intangible == false)
        {
            Ray emptyCheck = new Ray(transform.position, rayCheck);
            Debug.DrawRay(transform.position, rayCheck * 1f);

            //For everything that the raycast hits, starting from the player and going 1 square...
            foreach (RaycastHit hit in Physics.RaycastAll(emptyCheck, 1f))
            {
                //if it hits something, not including the player or floor they're standing on...
                if (hit.transform != transform && hit.transform.position != transform.position)
                {
                    Rotate(rayCheck);
                    Move(rayCheck);
                }
            }
        }
    }
}
