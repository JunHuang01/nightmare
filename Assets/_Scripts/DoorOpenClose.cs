using UnityEngine;
using System.Collections;

public class DoorOpenClose : MonoBehaviour {

    public float smooth;
    public float fieldOfViewAngle = 110.0f;
    private bool bIsDoorOpen = false;
    public bool isPlayerInPath = false;

    private Vector3 closePosition;
    private Vector3 openPosition;
    private Vector3 newPosition;

    private Transform RotatingAxisTransform;
    private GameObject player;
    private GameObject doorPathDetectionObj;

    private bool isPlayerNear;
    private BoxCollider doorCollider;
    private Vector3 transformDirection;
    private Vector3 transformDirection2;
    private Vector3 transformDirection3;
    private Vector3 transformToLeft;
    
    

    //Initialize variables
    void Awake() { 
        RotatingAxisTransform = transform.parent;
        newPosition = RotatingAxisTransform.eulerAngles;
        player = GameObject.FindGameObjectWithTag(Tags.player);
        doorPathDetectionObj = GameObject.FindGameObjectWithTag(Tags.DoorPathDetection);
        isPlayerNear = false;
        //doorCollider = GetComponent<BoxCollider>();
        transformDirection = new Vector3();
        transformDirection2 = new Vector3();
        transformDirection3 = new Vector3();

        transformToLeft = new Vector3(-1*0.5f, 0f, 0f);
        Debug.Log(collider.bounds.size.x);
    }

    void Update() {

    }

    void FixedUpdate() {
        //when press E key, we define what position the door should go to.
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            bIsDoorOpen = !bIsDoorOpen;
            isPlayerInPath = false;
            updatePathDetectionPos();
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
            RotatingAxisTransform.eulerAngles = Vector3.Lerp(currPosition, newPosition, (Time.time -startTime)/smooth);
            //RotatingAxisTransform.eulerAngles = newPosition;

            yield return null;
        }

        //Debug.Log(newPosition);
        isPlayerInPath = false;
        RotatingAxisTransform.eulerAngles = newPosition;
    }

    void updatePathDetectionPos() {
        //place the detection box in the right direction;
        if (bIsDoorOpen)
        {
            doorPathDetectionObj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.2f);
        }
        else {
            doorPathDetectionObj.transform.localPosition = new Vector3(0.0f, 0.0f, -0.2f);
        }
    }
}
