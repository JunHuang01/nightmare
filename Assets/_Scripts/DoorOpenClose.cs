using UnityEngine;
using System.Collections;

public class DoorOpenClose : MonoBehaviour {

    public float smooth; //how much time we use to open/close door

    private bool bIsDoorOpen = false;

    private Vector3 closePosition;
    private Vector3 openPosition;
    private Vector3 newPosition;

    private Transform RotatingAxisTransform;
    private GameObject player;

    private bool isPlayerNear;
    private BoxCollider doorCollider;
    private Vector3 transformDirection;


    private Vector3 transformToLeft;
    
    private bool isPlayerInPath;

    //Initialize variables
    void Awake() { 
        //Door's rotating axis transform information
        RotatingAxisTransform = transform.parent;

        //initialize the new position to the rotating axis's current angle
        newPosition = RotatingAxisTransform.eulerAngles;

        //find player object by the tag
        player = GameObject.FindGameObjectWithTag(Tags.player);

        //initialize variable setting to false indicating player is not near right now.
        isPlayerNear = false;

        //initialize transform direction
        transformDirection = new Vector3();

        //initialize that player is not in the door's closing / openning path
        isPlayerInPath = false;

        //define how we transform the door to left
        transformToLeft = new Vector3(-1*0.5f, 0f, 0f);
        
        
        //Debug.Log(collider.bounds.size.x);
    }

    void Update() {


    }

    void FixedUpdate() {
        //when press E key, we define what position the door should go to.
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            bIsDoorOpen = !bIsDoorOpen;
            isPlayerInPath = false;
            //Debug.Log("Now the door open is set to " + bIsDoorOpen);
            //this.StopCoroutine("DoorPositionChange");

            //stops the door movement if it is already in process of moving
            StopCoroutine("DoorPositionChange");

            //starts the door movement by the sub routine
            StartCoroutine("DoorPositionChange");
        }
    }


    void OnTriggerEnter(Collider other) {
        //whenever player get inside to the sphere collider , indicate player is near
        if (other.gameObject == player) {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //whenever player leave the sphere collider box, indicate player is far away
        if (other.gameObject == player)
        {
            isPlayerNear = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //when player hits the door stops the door movement if player is in the path of door's moving direction.
        if (col.gameObject == player && isPlayerInPath) {
            StopCoroutine("DoorPositionChange");
            
            //print("collided");
        }
    }


    //Setup which direction the door should go to
    void SetTransformDirection() {

        //when door is open, transform to open direction otherwise close
        if (bIsDoorOpen)
        {
            transformDirection = transform.parent.TransformDirection(0, 0, 1);
        }
        else {
            transformDirection = transform.parent.TransformDirection(0, 0, -1);
        }
    }

    //this function detects if player is in the path of door's moving direction
    void isPlayerInDoorTransformPath() {
        //declare 3 raycast one on the left, one on the right , and one in the middle, this misses in some cases.
        //ToDo: change this to a collider box to detect.
        RaycastHit hit;
        RaycastHit hit2;
        RaycastHit hit3;
                
        //cast a ray to see which object hit first, results is stored in hit
        if (Physics.Raycast(transform.parent.position + transform.up, transformDirection.normalized * 10, out hit))
        {
            //Debug.Log(hit.collider.gameObject);
            //If the raycast hits the player indicate that player is in path otherwise no;
            if (hit.collider.gameObject == player)
            {
                //Debug.Log("col happened!");
                //print("ray right hit");
                isPlayerInPath = true;
                Debug.DrawRay(transform.parent.position + transform.up, transformDirection.normalized * 10, Color.green);
            }
            else {
                //print("is in view angle");
                isPlayerInPath = false;
                Debug.DrawRay(transform.parent.position + transform.up, transformDirection.normalized * 10, Color.red);
            }
        }
        
        //set the left edge point
        Vector3 LeftEdgePoint = transform.TransformPoint(transformToLeft);

        //cast a ray to see which object hit first, results is stored in hit
        if (Physics.Raycast(LeftEdgePoint + transform.up, transformDirection.normalized * 10, out hit2))
        {
            //Debug.Log(hit2.collider.gameObject);
            //If the raycast hits the player indicate that player is in path otherwise no;
            if (hit2.collider.gameObject == player)
            {
                //Debug.Log("col happened!");
                //print("ray left hit");
                isPlayerInPath = true;
                Debug.DrawRay(LeftEdgePoint + transform.up, transformDirection.normalized * 10, Color.green);
            }
            else
            {
                //print("is in view angle");
                isPlayerInPath = false;
                Debug.DrawRay(LeftEdgePoint + transform.up, transformDirection.normalized * 10, Color.red);
            }
        }

        //cast a ray to see which object hit first, results is stored in hit
        if (Physics.Raycast(transform.position + transform.up, transformDirection.normalized * 10, out hit3))
        {
            //Debug.Log(hit3.collider.gameObject);
            //If the raycast hits the player indicate that player is in path otherwise no;
            if (hit3.collider.gameObject == player)
            {
                //print("ray middle hit");
                //Debug.Log("col happened!");
                isPlayerInPath = true;
                //Debug.DrawRay(transform.position + transform.up, transformDirection.normalized * 10, Color.green);
            }
            else
            {
                isPlayerInPath = false;
                //print("is in view angle");
                //Debug.DrawRay(transform.position + transform.up, transformDirection.normalized * 10, Color.red);
            }
        }
    }

    IEnumerator DoorPositionChange() {
        //get current time as starting time
        float startTime = Time.time;

        //define open and close position
        openPosition = new Vector3(RotatingAxisTransform.eulerAngles.x, 90.0f, RotatingAxisTransform.eulerAngles.z);
        closePosition = new Vector3(RotatingAxisTransform.eulerAngles.x, 0.0f, RotatingAxisTransform.eulerAngles.z);
        //get current position
        Vector3 currPosition = RotatingAxisTransform.eulerAngles;
        


        //set new target according to open or close action
        if (bIsDoorOpen)
        {
            newPosition = openPosition;
        }
        else
        {
            newPosition = closePosition;
        }
        //Debug.Log(currPosition + " is the current position");
        //Debug.Log(newPosition + " is the new posotioon");

        //while in the time frame to open/close door move the door to new target
        while (Time.time < startTime + smooth)
        {
            Debug.DrawRay(transform.parent.position + transform.up, transformDirection.normalized * 10, Color.blue);
            Debug.DrawRay(transform.position + transform.up, transformDirection.normalized * 10, Color.blue);

            //get left edge of the door
            Vector3 LeftEdgePoint = transform.TransformPoint(transformToLeft);
            Debug.DrawRay(LeftEdgePoint + transform.up, transformDirection.normalized * 10, Color.blue);
            //Debug.Log(isPlayerInPath);
            
            //finish the door open and close operation over time
            SetTransformDirection();
            isPlayerInDoorTransformPath();

            //rotate the door around the axis over time
            RotatingAxisTransform.eulerAngles = Vector3.Lerp(currPosition, newPosition, (Time.time -startTime)/smooth);
            //RotatingAxisTransform.eulerAngles = newPosition;

            yield return null;
        }

        //Debug.Log(newPosition);
        //reset player in path variable
        isPlayerInPath = false;

        //set to the new target as we are at the end of the transform action.
        RotatingAxisTransform.eulerAngles = newPosition;
    }
}
