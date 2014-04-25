using UnityEngine;
using System.Collections;

public class textPosition : MonoBehaviour {
    private GUIText menuText;
    public float wPos;
    public float hPos;
    void Awake() {
        menuText = GetComponent<GUIText>();
        //menuText.pixelOffset = new Vector2(Screen.width*(11/12),Screen.height*(11/12));
        menuText.pixelOffset = new Vector2(Screen.width*wPos, Screen.height*hPos);
    }
}
