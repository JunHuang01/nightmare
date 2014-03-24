using UnityEngine;
using System.Collections;

public class DoorOpenClose : MonoBehaviour {

    public float smooth;

    private bool bIsDoorOpen = false;

    Vector3 closePosition;
    Vector3 newPosition;

    void OnAwake() { 
         newPosition = transform.eulerAngles;
    }

    void Update() {
        DoorPositionChange();
    }
    void FixedUpdate() {
        
    }

    void DoorPositionChange() {

        //define open and close position
        Vector3 openPosition = new Vector3(transform.eulerAngles.x, 90.0f, transform.eulerAngles.z);
        Vector3 closePosition = new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z);


        //when press E key, we define what position the door should go to.
        if (Input.GetKeyDown(KeyCode.E)) {
            if (bIsDoorOpen)
            {
                newPosition = closePosition;
            }
            else
            {
                newPosition = openPosition;
            }
        }

        //flip the boolean for door state
        bIsDoorOpen = !bIsDoorOpen;

        //finish the door open and close operation over time
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, newPosition, smooth * Time.deltaTime);
    }
}
