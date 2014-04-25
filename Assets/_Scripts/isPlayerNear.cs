using UnityEngine;
using System.Collections;

public class isPlayerNear : MonoBehaviour {

    private DoorOpenClose doorScript;
    private GameObject player;
    void Awake() {
        doorScript = transform.parent.GetComponent<DoorOpenClose>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }

    void OnTriggerEnter(Collider other) {
        //when player enters activation zone, player is near
        if (other.gameObject == player) {
            doorScript.isPlayerNear = true;
        }    
    }

    void OnTriggerExit(Collider other) {
        //when player exits activation zone, player is not near
        if (other.gameObject == player) {
            doorScript.isPlayerNear = false;
        }
    }

}
