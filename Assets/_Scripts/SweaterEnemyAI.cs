using UnityEngine;
using System.Collections;

public class SweaterEnemyAI : MonoBehaviour {
    private bool isPlayerSighted;
    private Vector3 playerLastSightPos;
    private Vector3 playerNotSightedPos;

    private GameObject player;
    void Awake() {
        isPlayerSighted = false;
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerNotSightedPos = new Vector3(99999f, 99999f, 99999f);
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
            playerLastSightPos = player.transform.position;
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.yellow;
            playerLastSightPos = playerNotSightedPos;
        }
    }
}
