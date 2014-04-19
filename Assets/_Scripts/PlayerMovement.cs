using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float Speed; // player's movement speed
    public float turnSmoothing; // how long does it take foor player to turn
    public float speedDampTime = 0.1f; // how much time the speed is damped
    public float nextFire; //track when is the next fire
    public float fireRate = 1f; // how long between each shot can be fired
    public Transform shotSpawn; //position of where the bullets spawned
    public GameObject waterBullet; //bullet object that will be spawned

    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;

    //initialize variables
    void Awake() {
        anim = GetComponent<Animator>();
        //GetComponent<Animation>().wrapMode = WrapMode.Loop;

    }

    void FixedUpdate() {

        //get input of how much player is moving left, right and forward , backward
        float horMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        //move player according to player's input
        MovementManagement(horMovement, verMovement);

        //if fire is triggered, fire the bullets if we have waited long enough before our last one
        if (Input.GetButton("Fire1") && Time.time > nextFire) {

            // keep track of when can you fire the next bullet 
            nextFire = Time.time + fireRate; 

            //spawn the bullet
            Instantiate(waterBullet, shotSpawn.position, shotSpawn.rotation);  
        }
    }

    //move the character according to player input
    void MovementManagement(float horMovement, float verMovement) {

        //get char controller component
        CharacterController controller = GetComponent<CharacterController>();

        //if player has any input move otherwise put player on halt
        if (horMovement != 0f || verMovement != 0f)
        {
            //rotate the player according to left and right input
            transform.Rotate(0, horMovement * turnSmoothing, 0);

            //define how far forward and backward should the player travel by their respective input
            moveDirection = new Vector3(0, 0, verMovement);

            //define player's target transformation position
            moveDirection = transform.TransformDirection(moveDirection);

            //smooth the movement
            moveDirection *= 3.5f;

            //set the animation for the player's movement
            anim.SetFloat("Speed", 3.5f, speedDampTime, Time.deltaTime);
        }
        else {

            //stop the player's movement animation
            anim.SetFloat("Speed", 0);

            //set new direction to be stopped
            moveDirection = new Vector3(0, 0, 0);
        }

        //Move the player after everything is set
        controller.Move(moveDirection * Time.deltaTime);
    }
}
