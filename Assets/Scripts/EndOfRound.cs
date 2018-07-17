using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfRound : MonoBehaviour {

    private void Awake() {
        this.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextRound() {
        ScoreHandler.Instance.IsGamePaused = false;

        if (UpgradeMenuBehavior.currentlyOpenedUpgradeMenu != null)
            Destroy(UpgradeMenuBehavior.currentlyOpenedUpgradeMenu);

        Spawner.Instance.EnemyCount = 5;
        this.gameObject.SetActive(false);
    }

    public void BuyTurret() {
        if (ScoreHandler.Instance.GetMoney() < 50)
            return;
        ScoreHandler.Instance.ChangeMoney(-50);

        var turretPrefab = Resources.Load<GameObject>("Prefabs/missileLauncher");

        Instantiate(turretPrefab, new Vector3(0, -10, 0), Quaternion.identity);
    }
}
