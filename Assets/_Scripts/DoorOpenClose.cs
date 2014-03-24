using UnityEngine;
using System.Collections;

public class DoorOpenClose : MonoBehaviour {

    public float smooth;

    private bool bIsDoorOpen = false;

    Vector3 closePosition;
    Vector3 openPosition;
    Vector3 newPosition;

    void OnAwake() { 
         newPosition = transform.eulerAngles;

    }

    void Update() {
        DoorPositionChange();
    }
    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bIsDoorOpen = !bIsDoorOpen;
        }   
    }

    void DoorPositionChange() {
        //define open and close position
        openPosition = new Vector3(transform.eulerAngles.x, 90.0f, transform.eulerAngles.z);
        closePosition = new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z);

        

        //when press E key, we define what position the door should go to.
        if (bIsDoorOpen)
        {
            newPosition = openPosition;
        }
        else
        {
            newPosition = closePosition;
        }

        //finish the door open and close operation over time
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, newPosition, smooth * Time.deltaTime);
    }
}
