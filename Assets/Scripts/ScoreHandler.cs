using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {

    public Text scoreText;

    public Text healthText;

    public GameObject endOfRoundCanvasObject;

    public Text moneyText;

    private int score;
    private int health;
    private int money;

    private bool endingInProgress;

    private bool _isGamePaused;
    public bool IsGamePaused {
        get {
            return _isGamePaused;
        }
        set {
            if (!IsGameOver) {
                _isGamePaused = value;
            }
        }
    }

    public bool IsGameOver {
        get {
            return GetHealth() <= 0;
        }
        set {
            if (value) {
                int change = -GetHealth();
                ChangeHealth(change);
            }
        }
    }

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

    private void Start() {
        Instance = this;

        score = 0;
        ChangeScore(0);
        health = Settings.Instance.MaxHealth;
        ChangeHealth(0);
        money = Settings.Instance.DefaultMoney;
        ChangeMoney(0);

        IsGamePaused = true;

        if (Settings.Instance.IsLowFrameMode) {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 5;
        }
    }

    // Use this for initialization
    //void Start() {

    //}

    // Update is called once per frame
    void Update() {
        if (CanShowEndScreen()) {
            IsGamePaused = true;
            StartCoroutine(ShowRoundEndScreen());
        }
    }

    public int GetScore() {
        return score;
    }

    public int GetHealth() {
        return health;
    }

    public int GetMoney() {
        return money;
    }

    public void ChangeScore(int change) {
        score += change;
        scoreText.text = score.ToString();

        ChangeMoney(change);
    }

    public void ChangeHealth(int change) {
        health += change;
        if (health < 0)
            health = 0;

        healthText.text = health.ToString();

        if (health <= 0) {
            StartCoroutine(EndGame());
        }
    }

    public void ChangeMoney(int change) {
        money += change;

        moneyText.text = money.ToString() + "$";
    }

    public IEnumerator EndGame() {
        if (!endingInProgress) {
            endingInProgress = true;

            ShowEndingExplosion();
            ShowEndingText();

            yield return new WaitForSeconds(5.0f);
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void ShowEndingExplosion() {
        var explosionPrefab = Resources.Load<GameObject>("Explosions/Prefabs/ExplosionBig");
        GameObject o = Instantiate(explosionPrefab, new Vector3(0, 10, 0), Quaternion.identity);
        Destroy(o, 5);
    }

    private void ShowEndingText() {
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
    }

    //TODO extract this so it can be used in preventing shooting during inter-round menu
    private bool CanShowEndScreen() {
        bool haveSpawnsEnded = Spawner.Instance.GetEnemyCount() == 0;
        bool areEnemiesPresent = GameObject.FindGameObjectsWithTag("enemy").Length != 0;
        bool isPaused = IsGamePaused;

        return haveSpawnsEnded && !areEnemiesPresent && !isPaused && !IsGameOver;
    }

    public IEnumerator ShowRoundEndScreen() {
        yield return new WaitForSeconds(2);
        endOfRoundCanvasObject.SetActive(true);
    }
}
