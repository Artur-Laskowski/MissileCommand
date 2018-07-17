using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileTubeBehavior : MonoBehaviour {
    
    public GameObject parentLauncher;
    public Transform muzzle;
    public GameObject missile;

    private int roundsPerMinute;

    private float lastShotTime;
    private bool isPlaced;

    static private GameObject healthbarPrefab;
    static private GameObject upgradeButtonPrefab;
    static private GameObject gameCanvas;
    static private GameObject target;
    static private GameObject upgradeMenuPrefab;

    public int projectileSpeedLevel;
    public int explosionSizeLevel;
    public int accuracyLevel;

    private int _turretHealth;
    public int TurretHealth {
        get {
            return _turretHealth;
        }
        set {
            if (value < 0) {
                _turretHealth = 0;
                DestroyTurret();
                return;
            }

            _turretHealth = value;
            healthBar.GetComponent<Slider>().value = value / 100.0f;
        }
    }

    private GameObject healthBar;

    private int _rofLevel;
    public int RofLevel {
        get {
            return _rofLevel;
        }
        set {
            _rofLevel = value;
            roundsPerMinute = Settings.Instance.rofLevels[_rofLevel - 1];
        }
    }

    private GameObject upgradeButton;

    // Use this for initialization
    void Start () {
        isPlaced = false;

        //TODO refactor
        if (healthbarPrefab == null) {
            healthbarPrefab = Resources.Load<GameObject>("Prefabs/HealthBar");
        }
        if (upgradeButtonPrefab == null) {
            upgradeButtonPrefab = Resources.Load<GameObject>("Prefabs/UpgradeButton");
        }
        if (upgradeMenuPrefab == null) {
            upgradeMenuPrefab = Resources.Load<GameObject>("Prefabs/UpgradeCanvas");
        }
        projectileSpeedLevel = 1;
        explosionSizeLevel = 1;
        accuracyLevel = 1;
        RofLevel = 1;

        this.transform.SetParent(parentLauncher.transform);
        
        lastShotTime = Time.time;

        UserControls.Instance.PrimaryFire += FireStart;
        UserControls.Instance.PrimaryFireHeld += FireHeld;
        UserControls.Instance.PrimaryFire += PlaceTurret;
    }

    private void Awake() {
        if (gameCanvas == null) {
            gameCanvas = GameObject.Find("GameCanvas");
        }

        if (target == null) {
            target = GameObject.Find("target");
        }
    }

    // Update is called once per frame
    void Update () {
        if (!ScoreHandler.Instance.IsGamePaused)
            SetRotation(transform.position, target.transform.position);

        if (!isPlaced) {
            FollowCursor();
        }
    }
    //TODO use user input
    void FollowCursor() {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.parentLauncher.transform.position = new Vector3(position.x, 0, 0);
    }

    void PlaceTurret() {
        if (isPlaced)
            return;
        isPlaced = true;

        CreateHealthbar();
        CreateUpgradeButton();
    }

    void CreateHealthbar() {
        var worldPosition = this.transform.position + new Vector3(0, -0.5f, 0);
        var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        healthBar = Instantiate(healthbarPrefab, screenPosition, Quaternion.identity);
        healthBar.transform.SetParent(gameCanvas.transform);
        healthBar.transform.localScale = new Vector3(0.05f, 0.05f);

        TurretHealth = 100; //this will initialize healthbar value
    }

    void CreateUpgradeButton() {
        var worldPosition = this.transform.position + new Vector3(0, 3f, 0);
        var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        upgradeButton = Instantiate(upgradeButtonPrefab, screenPosition, Quaternion.identity);
        upgradeButton.transform.SetParent(gameCanvas.transform);
        upgradeButton.transform.localScale = new Vector3(0.15f, 0.15f);
        upgradeButton.GetComponent<Button>().onClick.AddListener(UpgradeButtonClicked);
    }

    void UpgradeButtonClicked() {
        if (UpgradeMenuBehavior.UpgradeMenuOpened)
            return;
        UpgradeMenuBehavior.UpgradeMenuOpened = true;
        var upgradeMenu = Instantiate(upgradeMenuPrefab);
        upgradeMenu.GetComponent<UpgradeMenuBehavior>().Initialize(this);
    }

    void SetRotation(Vector3 start, Vector3 end) {
        var dir = end - start;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    bool CanSpawnMissile() {
        float interval = 60.0f / roundsPerMinute;
        bool isAlive = ScoreHandler.Instance.GetHealth() > 0;
        bool isPaused = ScoreHandler.Instance.IsGamePaused;
            return Time.time > lastShotTime + interval && isAlive && !isPaused;
    }

    void FireStart() {
        if (CanSpawnMissile()) {
            SpawnMissile();
            lastShotTime += Random.Range(0.0f, 60.0f / roundsPerMinute);
        }
    }

    void FireHeld() {
        if (CanSpawnMissile()) {
            SpawnMissile();
        }
    }

    void SpawnMissile() {
        var settings = Settings.Instance;
        var speed = settings.projectileSpeedLevels[projectileSpeedLevel - 1];
        var accuracy = settings.accuracyLevels[accuracyLevel - 1];
        var explosionSize = settings.explosionSizeLevels[explosionSizeLevel - 1];
        GameObject missileObject = Instantiate(missile, muzzle.position, this.transform.rotation);
        MissileBehavior mb = missileObject.GetComponent<MissileBehavior>();
        mb.Initialize(speed, accuracy, explosionSize);

        lastShotTime = Time.time;
    }

    void DestroyTurret() {
        UserControls.Instance.PrimaryFire -= FireStart;
        UserControls.Instance.PrimaryFireHeld -= FireHeld;
        UserControls.Instance.PrimaryFire -= PlaceTurret;
        Destroy(this.parentLauncher.gameObject);
        Destroy(upgradeButton);
        Destroy(healthBar);
    }
}
