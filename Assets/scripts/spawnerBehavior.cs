using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerBehavior : MonoBehaviour {

    public Transform enemy;

    static private spawnerBehavior _instance;
    static public spawnerBehavior Instance {
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
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0, 100) < 2) {
            Instantiate(enemy, new Vector3(Random.Range(-20, 20), 25.0f, 0), Quaternion.identity);
            
        }
	}
}
