﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform enemy;

    private float lastSpawnTime;
    
    public int EnemyCount { get; set; }

    static private Spawner _instance;
    static public Spawner Instance {
        get {
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
        Instance = this;
        lastSpawnTime = Time.time;
    }

    // Use this for initialization
    void Start () {
        //enemy = FindObjectsOfTypeIncludingAssets()
        EnemyCount = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (CanSpawnEnemy()) {
            SpawnEnemy();
        }
	}

    bool CanSpawnEnemy() {
        var delay = 60.0f / Settings.Instance.EnemySpawnsPerMinute;
        bool timeReached = Time.time > lastSpawnTime + delay;
        bool pause = ScoreHandler.Instance.IsGamePaused;

        return timeReached && GetEnemyCount() > 0 && !pause;
    }

    void SpawnEnemy() {
        float xMin = Settings.Instance.XPosMin;
        float xMax = Settings.Instance.XPosMax;
        Instantiate(enemy, new Vector3(Random.Range(xMin + 2, xMax - 2), 25.0f, 0), Quaternion.identity);
        lastSpawnTime = Time.time;

        ChangeEnemyCount(-1);
    }

    public int GetEnemyCount() {
        return EnemyCount;
    }

    public void ChangeEnemyCount(int change) {
        if (EnemyCount >= change) {
            EnemyCount += change;
        }

    }
}
