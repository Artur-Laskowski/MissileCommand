using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform enemy;

    private float lastSpawnTime;

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
