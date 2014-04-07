using UnityEngine;
using System.Collections;

public class DoorOpenClose : MonoBehaviour {

    public float smooth;

    private bool bIsDoorOpen = false;

    private Vector3 closePosition;
    private Vector3 openPosition;
    private Vector3 newPosition;

    private Transform RotatingAxisTransform;
    private GameObject player;

    private bool isPlayerNear;
    private BoxCollider doorCollider;

    //Initialize variables
    void Awake() { 
        RotatingAxisTransform = transform.parent;
        newPosition = RotatingAxisTransform.eulerAngles;
        player = GameObject.FindGameObjectWithTag(Tags.player);
        isPlayerNear = false;
        //doorCollider = GetComponent<BoxCollider>();
        
    }

    void Update() {
        
    }

    void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            bIsDoorOpen = !bIsDoorOpen;
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
        if (col.gameObject == player) {
            Debug.Log("col happened!");
            StopCoroutine("DoorPositionChange");
        }
    }

    IEnumerator DoorPositionChange() {
        
        float startTime = Time.time;

        //define open and close position
        openPosition = new Vector3(RotatingAxisTransform.eulerAngles.x, 90.0f, RotatingAxisTransform.eulerAngles.z);
        closePosition = new Vector3(RotatingAxisTransform.eulerAngles.x, 0.0f, RotatingAxisTransform.eulerAngles.z);
        Vector3 currPosition = RotatingAxisTransform.eulerAngles;
        //when press E key, we define what position the door should go to.



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
            //finish the door open and close operation over time
            RotatingAxisTransform.eulerAngles = Vector3.Lerp(currPosition, newPosition, (Time.time -startTime)/smooth);
            //RotatingAxisTransform.eulerAngles = newPosition;

            yield return null;
        }

        //Debug.Log(newPosition);

        RotatingAxisTransform.eulerAngles = newPosition;
    }
}
