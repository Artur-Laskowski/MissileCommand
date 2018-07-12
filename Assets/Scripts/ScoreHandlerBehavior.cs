using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        ChangeScore(0);
        health = Settings.Instance.MaxHealth;
        ChangeHealth(0);

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
        if (health + change < 0)
            return;

        health += change;
        healthText.text = health.ToString();

        if (health <= 0) {
            StartCoroutine(EndGame());
        }
    }

    public IEnumerator EndGame() {
        //show text
        GameObject gameOverText = new GameObject("GameOverText");
        GameObject canvas = GameObject.Find("Canvas");
        gameOverText.transform.SetParent(canvas.transform);
        gameOverText.transform.localPosition = new Vector3(0, 0, 0);

        Text t = gameOverText.AddComponent<Text>();
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        t.font = ArialFont;
        t.material = ArialFont.material;
        t.color = new Color(0, 0, 0);
        t.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        t.text = "GAME OVER\n\nSCORE: " + score;
        t.transform.localPosition = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
