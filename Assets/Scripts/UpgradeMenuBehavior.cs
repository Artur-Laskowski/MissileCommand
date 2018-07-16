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

    private int ProjectileSpeedLevel {
        get {
            return selectedTurret.projectileSpeedLevel;
        }
        set {
            selectedTurret.projectileSpeedLevel = value;
            projectileSpeed.text = value.ToString();
            projectileSpeedCost.text = (value * 10).ToString();
        }
    }

    private int ExplosionSizeLevel {
        get {
            return selectedTurret.explosionSizeLevel;
        }
        set {
            selectedTurret.explosionSizeLevel = value;
            explosionSize.text = value.ToString();
            explosionSizeCost.text = (value * 10).ToString();
        }
    }

    private int AccuracyLevel {
        get {
            return selectedTurret.accuracyLevel;
        }
        set {
            selectedTurret.accuracyLevel = value;
            accuracy.text = value.ToString();
            accuracyCost.text = (value * 10).ToString();
        }
    }

    private int RateOfFireLevel {
        get {
            return selectedTurret.RofLevel;
        }
        set {
            selectedTurret.RofLevel = value;
            rateOfFire.text = value.ToString();
            rateOfFireCost.text = (value * 10).ToString(); //TODO costs
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpgradeProjectileSpeed() {
        ProjectileSpeedLevel++;
    }

    public void UpgradeExplosionSize() {
        ExplosionSizeLevel++;
    }

    public void UpgradeAccuracy() {
        AccuracyLevel++;
    }

    public void UpgradeRateOfFire() {
        RateOfFireLevel++;
    }

    public void Close() {
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
