using UnityEngine;
using System.Collections;

public class healthbar : MonoBehaviour {
	
	private int maxHp = 100;
	public int currHp = 100;
	
	private float BarLength;
	
	// Use this for initialization
	void Start () {

        //how long the health bar should be
		BarLength = Screen.width / 4;
	}
	
    // Update is called once per frame
	void Update () {
        // change the player's health
		AdjustHealth(0);
	}
	
	void OnGUI() {
        //draw health bar on the gui
		GUI.Box(new Rect(10,10, 10+BarLength, 20), "");
		GUI.Box (new Rect (10, 10, 10+(Screen.width / 4), 20), currHp.ToString());
	}
	
	public void AdjustHealth(int adj) {
        //change the player's HP.  And then make sure the health is not in an invalid state.
		currHp += adj;
		if(currHp < 0)
			currHp = 0;
		if(currHp > maxHp)
			currHp = maxHp;
		if(maxHp < 1)
			maxHp = 1;

        //change the health bar percentage according to current hp
		BarLength = (Screen.width / 4)*(currHp / (float)maxHp);
	}
}

