using UnityEngine;
using System.Collections;

public class enemymovement : MonoBehaviour {

	public float speed = 1;
	public Vector3 movedir = Vector3.back;
	
	void Start () {
		movedir = Vector3.forward;
	}
	
	void Update() {
		if (transform.position.z > 5f && movedir != Vector3.back) {
			movedir = Vector3.back;
			transform.Rotate(0,180,0);
		}
		if (transform.position.z < 0f && movedir != Vector3.forward) {
			movedir = Vector3.forward;
			transform.Rotate(0,180,0);
		}
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
