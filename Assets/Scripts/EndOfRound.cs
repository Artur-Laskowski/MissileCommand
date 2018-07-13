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
        Spawner.Instance.enemyCount = 5;
        this.gameObject.SetActive(false);
    }
}
