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


    static private ScoreHandlerBehavior _instance;
    static public ScoreHandlerBehavior Instance {
        get {
            if (_instance == null)
                throw new System.Exception("Tried to access singleton without instance");
            return _instance;
        }
        private set {
            if (_instance == null) {
                _instance = value;
            }
            else {
                Destroy(value.gameObject);
                throw new System.Exception("Second instance of singleton detected");
            }
        }
    }

    // Use this for initialization
    void Start () {
        score = 0;
        health = Settings.Instance.MaxHealth;

        Instance = this;

        if (Settings.Instance.IsLowFrameMode) {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 5;
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
