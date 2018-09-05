using UnityEngine;
using System.Collections;

public class CharAnim : MonoBehaviour {

    private Animator animComp;
    private float animState;
    private bool isWalking;
    public Vector3 pos;
    float speed = 2;
    float time = 0;
    public bool intangible = false;
    public bool playerMove = true;

    // Variables for particle use
    public SkinnedMeshRenderer[] listOfMeshRender;
    public ParticleSystem particle;
    public ParticleSystem.EmissionModule emmod;

    // Sounds
    //public AudioClip portalSound;
    //private AudioSource source;

    // Mobile
    private Vector2 touchOrigin = -Vector2.one;
    int horizontal = 0;
    int vertical = 0;

    // Use this for initialization
    void Start () {

        animComp = this.GetComponent<Animator>();
        pos = transform.position;
        listOfMeshRender = GetComponentsInChildren<SkinnedMeshRenderer>();
        particle = GetComponentInChildren<ParticleSystem>();
        emmod = particle.emission;

        //source = GetComponent<AudioSource>();

    }
	
    //Attempt move for mobile, convert swipes into vector directions to check.
    void AttemptMove(int horizontal, int vertical)
    {
        if (horizontal == 1)
        {
            ray(Vector3.forward);
        }

        else if (horizontal == -1)
        {
            ray(Vector3.back);
        }

        else if (vertical == 1)
        {
            ray(Vector3.left);
        }

        else if (vertical == -1)
        {
            ray(Vector3.right);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                //Set touchEnd to equal the position of this touch
                Vector2 touchEnd = myTouch.position;

                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;

                //Calculate the difference between the beginning and end of the touch on the y axis.
                float y = touchEnd.y - touchOrigin.y;

                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                touchOrigin.x = -1;

                //Check if the difference along the x axis is greater than the difference along the y axis.
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                    horizontal = x > 0 ? 1 : -1;
                else
                    //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                    vertical = y > 0 ? 1 : -1;
            }
        }

        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove(horizontal, vertical);
            horizontal = 0;
            vertical = 0;
        }

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

    void ray(Vector3 rayCheck)
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
}
