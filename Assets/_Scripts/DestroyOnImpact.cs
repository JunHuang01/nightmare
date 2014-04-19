using UnityEngine;
using System.Collections;

public class DestroyOnImpact : MonoBehaviour {


	void OnCollisionEnter (Collision col)
	{
        //anytime bullets anything it is destroyed
		Destroy (gameObject);

        //if we hit any of our enemy, they take demage!
        if (col.gameObject.tag == Tags.CandleEnemy || col.gameObject.tag == Tags.NormalEnemy) {
            EnemyStats currEnemyStats = col.gameObject.GetComponent<EnemyStats>();
            currEnemyStats.AttackedByPlayer();
        }
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}


}
