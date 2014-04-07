using UnityEngine;
using System.Collections;

public class SweaterEnemyAI : MonoBehaviour {
    private bool isPlayerSighted;
    void Awake() {
        isPlayerSighted = false;
    }

    void OnTriggerEnter() {
        isPlayerSighted = true;
    }

    void OnTriggerExit() {
        isPlayerSighted = false;
    }

    void Update() {
        OnPlayerSighted();
    }

    //ToDo: rewrite this function for proper Enemy Response
    void OnPlayerSighted() {

        //Testing to see if player enters in sight testing cube change color
        if (isPlayerSighted)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.yellow;
        }
    }
}
