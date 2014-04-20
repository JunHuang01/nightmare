using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    public int AttackDmg = 10;
    public int Health = 30;

    private bool isAttaccking = false;
    //private EnemyAI EnemyAIScript;
    private PlayerStats playerStats;
    private GameObject player;
	// Use this for initialization
	void Start () {
	
	}

    void Awake() {
        //refernece the enemy AI script;
        //EnemyAIScript = GetComponent<EnemyAI>();

        //reference to the player object
        player = GameObject.FindGameObjectWithTag(Tags.player);

        //reference the player stats script;
        playerStats = player.GetComponent<PlayerStats>();

        isAttaccking = false;
        

    }
	// Update is called once per frame
	void Update () {
        OnAttackPlayer();
        updateEnemyStatus();
	}

    void OnAttackPlayer() {
        //set the enemy attack animation;
        if (isAttaccking)
        {
            print("attacked by enemy");
            playerStats.OnAttackedByEnemy(AttackDmg);
        }

        //Once Attacked stop attack until told to do so again.
        StopEnemyAttack();
    }
    void updateEnemyStatus() {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void AttackedByPlayer() {
        Health -= playerStats.AttackDemmage;
    }

    public void StartEnemyAttack() { 
        //set enemy attacking to be true;
        isAttaccking = true;
    }

    void StopEnemyAttack() {
        isAttaccking = false;
    }
}
