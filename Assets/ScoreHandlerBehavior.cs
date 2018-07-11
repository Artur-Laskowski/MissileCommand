using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandlerBehavior : MonoBehaviour {
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text healthText;
    
    private int score;
    private int health;

    static public ScoreHandlerBehavior Instance { get; private set; }

    // Use this for initialization
    void Start () {
        score = 0;
        health = 100;

        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this.gameObject);
            throw new System.Exception("Second instance of singleton detected");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetScore() {
        return score;
    }

    public int GetHealth() {
        return health;
    }

    public void ChangeScore(int change) {
        score += change;
        scoreText.text = score.ToString();
    }

    public void ChangeHealth(int change) {
        health += change;
        healthText.text = health.ToString();
    }
}
