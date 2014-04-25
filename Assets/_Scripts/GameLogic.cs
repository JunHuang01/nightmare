using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    void Awake() {
        Screen.lockCursor = true;
        //Application.LoadLevel("nightmare");
    }
	// Use this for initialization
	void Start () {
	    
	}

    void OnApplicationFocus(bool focused)
    {
        Screen.lockCursor = true;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel("nightmare");
        }
	}
}
