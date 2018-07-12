using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private GameObject canvasObject;
    
    private int score;
    private int health;

    static private ScoreHandler _instance;
    static public ScoreHandler Instance {
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

    private void Awake() {
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

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (CanShowEndScreen()) {
            StartCoroutine(ShowRoundEndScreen());
        }
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
        GameObject canvas = GameObject.Find("GameCanvas");
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
    //TODO extract this so it can be used in preventing shooting during inter-round menu
    private bool CanShowEndScreen() {
        bool haveSpawnsEnded = Spawner.Instance.GetEnemyCount() == 0;
        bool areEnemiesPresent = GameObject.FindGameObjectsWithTag("enemy").Length != 0;

        return haveSpawnsEnded && !areEnemiesPresent;
    }

    public IEnumerator ShowRoundEndScreen() {
        yield return new WaitForSeconds(2);
        canvasObject.SetActive(true);
    }
}
