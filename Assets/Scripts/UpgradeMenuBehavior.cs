using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuBehavior : MonoBehaviour {

    public Text projectileSpeed;
    public Text projectileSpeedCost;
    public Text explosionSize;
    public Text explosionSizeCost;
    public Text accuracy;
    public Text accuracyCost;
    public Text rateOfFire;
    public Text rateOfFireCost;

    public MissileTubeBehavior selectedTurret;

    public static bool UpgradeMenuOpened = false;

    static public GameObject currentlyOpenedUpgradeMenu;

    private int ProjectileSpeedLevel {
        get {
            return selectedTurret.projectileSpeedLevel;
        }
        set {
            if (value > 10)
                return;
            selectedTurret.projectileSpeedLevel = value;
            projectileSpeed.text = value.ToString();
            string newCost = "max";
            if (value < 10)
                newCost = Settings.Instance.upgradeCosts[value].ToString() + "$";
            projectileSpeedCost.text = newCost;
        }
    }

    private int ExplosionSizeLevel {
        get {
            return selectedTurret.explosionSizeLevel;
        }
        set {
            if (value > 10)
                return;
            selectedTurret.explosionSizeLevel = value;
            explosionSize.text = value.ToString();
            string newCost = "max";
            if (value < 10)
                newCost = Settings.Instance.upgradeCosts[value].ToString() + "$";
            explosionSizeCost.text = newCost;
        }
    }

    private int AccuracyLevel {
        get {
            return selectedTurret.accuracyLevel;
        }
        set {
            if (value > 10)
                return;
            selectedTurret.accuracyLevel = value;
            accuracy.text = value.ToString();
            string newCost = "max";
            if (value < 10)
                newCost = Settings.Instance.upgradeCosts[value].ToString() + "$";
            accuracyCost.text = newCost;
        }
    }

    private int RateOfFireLevel {
        get {
            return selectedTurret.RofLevel;
        }
        set {
            if (value > 10)
                return;
            selectedTurret.RofLevel = value;
            rateOfFire.text = value.ToString();
            string newCost = "max";
            if (value < 10)
                newCost = Settings.Instance.upgradeCosts[value].ToString() + "$";
            rateOfFireCost.text = newCost;
        }
    }

    // Use this for initialization
    void Start () {
        currentlyOpenedUpgradeMenu = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpgradeProjectileSpeed() {
        var newLevel = ProjectileSpeedLevel + 1;
        var upgradeCost = Settings.Instance.upgradeCosts[newLevel - 1];
        var money = ScoreHandler.Instance.GetMoney();
        if (upgradeCost > money)
            return;

        ScoreHandler.Instance.ChangeMoney(-upgradeCost);
        
        ProjectileSpeedLevel++;
    }

    public void UpgradeExplosionSize() {
        var newLevel = ExplosionSizeLevel + 1;
        var upgradeCost = Settings.Instance.upgradeCosts[newLevel - 1];
        var money = ScoreHandler.Instance.GetMoney();
        if (upgradeCost > money)
            return;

        ScoreHandler.Instance.ChangeMoney(-upgradeCost);

        ExplosionSizeLevel++;
    }

    public void UpgradeAccuracy() {
        var newLevel = AccuracyLevel + 1;
        var upgradeCost = Settings.Instance.upgradeCosts[newLevel - 1];
        var money = ScoreHandler.Instance.GetMoney();
        if (upgradeCost > money)
            return;

        ScoreHandler.Instance.ChangeMoney(-upgradeCost);

        AccuracyLevel++;
    }

    public void UpgradeRateOfFire() {
        var newLevel = RateOfFireLevel + 1;
        var upgradeCost = Settings.Instance.upgradeCosts[newLevel - 1];
        var money = ScoreHandler.Instance.GetMoney();
        if (upgradeCost > money)
            return;

        ScoreHandler.Instance.ChangeMoney(-upgradeCost);

        RateOfFireLevel++;
    }

    public void Close() {
        UpgradeMenuOpened = false;
        Destroy(this.gameObject);
    }

    public void Initialize(MissileTubeBehavior launcher) {
        selectedTurret = launcher;
        ProjectileSpeedLevel = launcher.projectileSpeedLevel;
        ExplosionSizeLevel = launcher.explosionSizeLevel;
        AccuracyLevel = launcher.accuracyLevel;
        RateOfFireLevel = launcher.RofLevel;
    }
}
