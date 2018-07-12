using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform enemy;

    private float lastSpawnTime;

    //TODO extract this
    private int enemyCount;

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
        enemyCount = 1;
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

        return timeReached && GetEnemyCount() > 0;
    }

    void SpawnEnemy() {
        Instantiate(enemy, new Vector3(Random.Range(-20, 20), 25.0f, 0), Quaternion.identity);
        lastSpawnTime = Time.time;

        ChangeEnemyCount(-1);
    }

    public int GetEnemyCount() {
        return enemyCount;
    }

    public void ChangeEnemyCount(int change) {
        if (enemyCount >= change) {
            enemyCount += change;
        }

    }
}
