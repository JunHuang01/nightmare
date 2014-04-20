using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {
    public int candleAttack;

    void Awake() {
        candleAttack = Animator.StringToHash("CandleAttack");
    }
}
