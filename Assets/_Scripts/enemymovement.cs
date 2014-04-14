using UnityEngine;
using System.Collections;

public class enemymovement : MonoBehaviour {

	public float speed = 1f;
	public Vector3 movedir = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z > 3f && movedir != Vector3.back) {
			movedir = Vector3.back;
			transform.Rotate(0,180,0);
	}
		if (transform.position.z < 0f && movedir != Vector3.forward) {
			movedir = Vector3.forward;
			//transform.Rotate(0,180,0);
		}
		transform.Translate (movedir * speed * Time.deltaTime);
	}
}
