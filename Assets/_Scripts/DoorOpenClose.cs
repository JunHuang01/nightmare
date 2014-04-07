using UnityEngine;
using System.Collections;

public class DoorOpenClose : MonoBehaviour {

    public float smooth;
    public float fieldOfViewAngle = 110.0f;
    private bool bIsDoorOpen = false;

    private Vector3 closePosition;
    private Vector3 openPosition;
    private Vector3 newPosition;

    private Transform RotatingAxisTransform;
    private GameObject player;

    private bool isPlayerNear;
    private BoxCollider doorCollider;
    private Vector3 transformDirection;
    private Vector3 transformDirection2;
    private Vector3 transformDirection3;
    private Vector3 transformToLeft;
    
    private bool isPlayerInPath;

    //Initialize variables
    void Awake() { 
        RotatingAxisTransform = transform.parent;
        newPosition = RotatingAxisTransform.eulerAngles;
        player = GameObject.FindGameObjectWithTag(Tags.player);
        isPlayerNear = false;
        //doorCollider = GetComponent<BoxCollider>();
        transformDirection = new Vector3();
        transformDirection2 = new Vector3();
        transformDirection3 = new Vector3();
        isPlayerInPath = false;
        transformToLeft = new Vector3(-1*0.5f, 0f, 0f);
        Debug.Log(collider.bounds.size.x);
    }

    void Update() {
        /*Vector3 fwd = new Vector3(0, 0, 1);
        var forward = transform.TransformDirection(fwd) * 10;
        Debug.DrawRay(transform.position + transform.up, fwd * 10, Color.green);
         */

        //Debug.DrawRay(transform.position + transform.up, transformDirection.normalized * 10 , Color.blue);
        //SetTransformDirection();


    }

    void FixedUpdate() {
        //when press E key, we define what position the door should go to.
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            bIsDoorOpen = !bIsDoorOpen;
            isPlayerInPath = false;
            //Debug.Log("Now the door open is set to " + bIsDoorOpen);
            //this.StopCoroutine("DoorPositionChange");
            StopCoroutine("DoorPositionChange");
            StartCoroutine("DoorPositionChange");
        }
    }


    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerNear = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //print("COLLIDED!");
        if (col.gameObject == player && isPlayerInPath) {
            StopCoroutine("DoorPositionChange");
            
            //print("collided");
        }
    }


    void SetTransformDirection() {
        Vector3 LeftEdgePoint = transform.TransformPoint(transformToLeft);
        Transform tempTransform = transform;
        if (bIsDoorOpen)
        {
            transformDirection = transform.parent.TransformDirection(0, 0, 1);
            transformDirection = transform.TransformDirection(0, 0, 1);
            transformDirection = transform.TransformDirection(0, 0, 1);
        }
        else {
            transformDirection = transform.parent.TransformDirection(0, 0, -1);
        }
    }

    void isPlayerInDoorTransformPath() {
        RaycastHit hit;
        RaycastHit hit2;
        RaycastHit hit3;
                
        // ... and if a raycast towards the player hits something...
        if (Physics.Raycast(transform.parent.position + transform.up, transformDirection.normalized * 10, out hit))
        {
            Debug.Log(hit.collider.gameObject);
            // ... and if the raycast hits the player...
            if (hit.collider.gameObject == player)
            {
                //Debug.Log("col happened!");
                print("ray right hit");
                isPlayerInPath = true;
                Debug.DrawRay(transform.parent.position + transform.up, transformDirection.normalized * 10, Color.green);
            }
            else {
                //print("is in view angle");
                Debug.DrawRay(transform.parent.position + transform.up, transformDirection.normalized * 10, Color.red);
            }
        }
        
        Vector3 LeftEdgePoint = transform.TransformPoint(transformToLeft);
        
        if (Physics.Raycast(LeftEdgePoint + transform.up, transformDirection.normalized * 10, out hit2))
        {
            Debug.Log(hit2.collider.gameObject);
            // ... and if the raycast hits the player...
            if (hit2.collider.gameObject == player)
            {
                //Debug.Log("col happened!");
                print("ray left hit");
                isPlayerInPath = true;
                Debug.DrawRay(LeftEdgePoint + transform.up, transformDirection.normalized * 10, Color.green);
            }
            else
            {
                //print("is in view angle");
                Debug.DrawRay(LeftEdgePoint + transform.up, transformDirection.normalized * 10, Color.red);
            }
        }

        if (Physics.Raycast(transform.position + transform.up, transformDirection.normalized * 10, out hit3))
        {
            Debug.Log(hit3.collider.gameObject);
            // ... and if the raycast hits the player...
            if (hit3.collider.gameObject == player)
            {
                print("ray middle hit");
                //Debug.Log("col happened!");
                isPlayerInPath = true;
                Debug.DrawRay(transform.position + transform.up, transformDirection.normalized * 10, Color.green);
            }
            else
            {
                //print("is in view angle");
                Debug.DrawRay(transform.position + transform.up, transformDirection.normalized * 10, Color.red);
            }
        }
    }

    IEnumerator DoorPositionChange() {
        
        float startTime = Time.time;

        //define open and close position
        openPosition = new Vector3(RotatingAxisTransform.eulerAngles.x, 90.0f, RotatingAxisTransform.eulerAngles.z);
        closePosition = new Vector3(RotatingAxisTransform.eulerAngles.x, 0.0f, RotatingAxisTransform.eulerAngles.z);
        Vector3 currPosition = RotatingAxisTransform.eulerAngles;
        



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

        while (Time.time < startTime + smooth)
        {
            Debug.DrawRay(transform.parent.position + transform.up, transformDirection.normalized * 10, Color.blue);
            Debug.DrawRay(transform.position + transform.up, transformDirection.normalized * 10, Color.blue);
            Vector3 LeftEdgePoint = transform.TransformPoint(transformToLeft);
            Debug.DrawRay(LeftEdgePoint + transform.up, transformDirection.normalized * 10, Color.blue);
            Debug.Log(isPlayerInPath);
            //finish the door open and close operation over time
            SetTransformDirection();
            isPlayerInDoorTransformPath();

            RotatingAxisTransform.eulerAngles = Vector3.Lerp(currPosition, newPosition, (Time.time -startTime)/smooth);
            //RotatingAxisTransform.eulerAngles = newPosition;

            yield return null;
        }

        //Debug.Log(newPosition);
        isPlayerInPath = false;
        RotatingAxisTransform.eulerAngles = newPosition;
    }
}
