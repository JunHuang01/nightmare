using UnityEngine;
using System.Collections;

public class playerShooting : MonoBehaviour {
    public float nextFire;
    public GameObject waterBullet; //bullet object that will be spawned
    public Transform shotSpawn; //position of where the bullets spawned
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {

            // keep track of when can you fire the next bullet 
            nextFire = Time.time + GlobalConstant.nextPlayerFireRate;

            //spawn the bullet
            Instantiate(waterBullet, shotSpawn.position, shotSpawn.rotation);
        }
	}
}
