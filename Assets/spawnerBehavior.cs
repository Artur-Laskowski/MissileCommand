using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerBehavior : MonoBehaviour {

    public Transform enemy;

	// Use this for initialization
	void Start () {
		//enemy = FindObjectsOfTypeIncludingAssets()
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0, 100) < 2) {
            Instantiate(enemy, new Vector3(Random.Range(-20, 20), 25.0f, 0), Quaternion.identity);
            
        }
	}
}
