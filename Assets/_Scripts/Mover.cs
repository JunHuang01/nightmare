using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
        //move the object forward with given speed
        rigidbody.velocity = transform.forward * speed;
	}
}
