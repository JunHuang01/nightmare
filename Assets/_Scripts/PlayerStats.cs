using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
    public int AttackDemmage = 10;
    public int Health = 100;
	public int timer = 0;
    private PlayerHealthBar playerHealthBar;
    private GameObject player;
    private Animator playerAnim;

    void Awake() {
        //reference player health bar script
        playerHealthBar = GetComponent<PlayerHealthBar>();

        //get reference to player object
        player = GameObject.FindGameObjectWithTag(Tags.player);

        playerAnim = player.GetComponent<Animator>();

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        updatePlayerStatus();
	}

    void updatePlayerStatus() {

        //update this to player health bar
		timer++;
		if (timer > 100) {
			Health++;
			timer = 0;
		}
        playerHealthBar.AdjustHealth(Health);

        if (Health <= 0) {
            playerAnim.SetBool("isDead", true);

            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void OnAttackedByEnemy(int demageTaken) {

        //calc new hit point by demage Taken
        Health -= demageTaken;

        
    }
}
