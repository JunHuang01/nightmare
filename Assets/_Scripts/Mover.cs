using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	// Use this for initialization
	void Start () {
        //move the object forward with given speed
        rigidbody.velocity = transform.forward * GlobalConstant.bulletsSpeed;
	}
}
