using UnityEngine;
using System.Collections;

public class WinZoneScript : MonoBehaviour {
	
	bool entered = false;
	public GameObject winText;
	//This is trigger when any object enters the collider box
	void OnTriggerEnter (Collider collider)
	{
		Debug.Log (collider.tag);
		//Requires the object to be a tag type player
		if(collider.tag == "Player")
		{
			//Show win text 
			entered = true;
			Debug.Log ("ENTERED!!!");
		}

	}


	void Update(){
		if(entered){
			winText.SetActive(true); 
			Time.timeScale = 0;


            if (Input.GetKeyDown("space"))
            {
                Application.LoadLevel("MainMenu");
            }
		}	
	}

}
