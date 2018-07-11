using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour {

    public Transform enemy;

    private float lastSpawnTime;

    static private SpawnerBehavior _instance;
    static public SpawnerBehavior Instance {
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

    // Use this for initialization
    void Start () {
        //enemy = FindObjectsOfTypeIncludingAssets()
        Instance = this;
        lastSpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        var delay = 60.0f / Settings.Instance.EnemySpawnsPerMinute;
        if (Time.time > lastSpawnTime + delay) {
            SpawnEnemy();
        }
	}

    void SpawnEnemy() {
        Instantiate(enemy, new Vector3(Random.Range(-20, 20), 25.0f, 0), Quaternion.identity);
        lastSpawnTime = Time.time;
    }
}
