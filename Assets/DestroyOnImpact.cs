using UnityEngine;
using System.Collections;

public class DestroyOnImpact : MonoBehaviour {


	void OnCollisionEnter (Collision col)
	{
		Destroy (gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}


}
