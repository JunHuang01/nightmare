using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
    public int AttackDemmage = 10;
    public int MaxHealth = 100;
    public int Health;
	public int timer = 0;
    private float deathTimer = 0.0f;
    private PlayerHealthBar playerHealthBar;
    private GameObject player;
    private Animator playerAnim;
    public GameObject losingText;

    void Awake() {
        //reference player health bar script
        playerHealthBar = GetComponent<PlayerHealthBar>();

        //get reference to player object
        player = GameObject.FindGameObjectWithTag(Tags.player);

        playerAnim = player.GetComponent<Animator>();

        Health = MaxHealth;

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

		if (timer > 100 && Health < MaxHealth ) {
			Health++;
			timer = 0;
		}
        playerHealthBar.AdjustHealth(Health);

        if (Health <= 0  ) {
            playerAnim.SetBool("isDead", true);

            if (deathTimer == 0)
            {
                losingText.SetActive(true);
                deathTimer = Time.time + 5.0f;
            }
            
            

            if (Time.time> deathTimer)
            {
                deathTimer = 0.0f;
                losingText.SetActive(false);
                Application.LoadLevel("MainMenu");
            }
        }
    }

    public void OnAttackedByEnemy(int demageTaken) {

        //calc new hit point by demage Taken
        Health -= demageTaken;

        
    }
}
