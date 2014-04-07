using UnityEngine;
using System.Collections;

public class PlayerInDoorPathDetection : MonoBehaviour {

    private GameObject[] doorObjects;
    private GameObject thisDoorObject;
    private GameObject player;
    DoorOpenClose doorScriptRef;

    void Awake() {
        doorObjects = GameObject.FindGameObjectsWithTag(Tags.Door);
        player = GameObject.FindGameObjectWithTag(Tags.player);

        foreach (GameObject currDoorObj in doorObjects){
            if (currDoorObj.transform.parent == transform.parent.parent)
                thisDoorObject = currDoorObj;
        }
        doorScriptRef = thisDoorObject.GetComponent<DoorOpenClose>();
    }

    void OnTriggerStay(Collider other) {
        //when player is in the detection box set player in path to true;
        if (other.gameObject == player)
            doorScriptRef.isPlayerInPath = true;
    }

    void onTriggerExit(Collider other) {
        //when player is not in the detection box set player in path to false;
        if (other.gameObject == player)
            doorScriptRef.isPlayerInPath = false;
    }
}
