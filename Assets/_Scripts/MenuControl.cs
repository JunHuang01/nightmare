using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {
    public bool isPaused = true; //is game paused
    
    //init variable
    void Start(){
        isPaused = true;
    }

    //draw gui button
    void OnGUI() {

        //draw buttons only on paused
        if (this.isPaused)
        {
            //start button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 20), "Start Game"))
            {
                this.isPaused = false;
                Application.LoadLevel("nightmare");
                
            }
            //Options
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 30, 200, 20), "Options"))
            {

            }

            //Credits button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 60, 200, 20), "Credits"))
            {

            }

            //Exit button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 90, 200, 20), "Exit"))
            {
                Application.Quit();
            }

        }
    }
}
