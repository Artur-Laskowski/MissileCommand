using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileShot : MonoBehaviour {

    public GameObject missile;
    public GameObject missileLauncher;

    private List<GameObject> spawnedMissiles;

	// Use this for initialization
	void Start () {
        spawnedMissiles = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnMissile();
        }

        spawnedMissiles.ForEach((GameObject m) => m.transform.position += new Vector3(0, 0.1f, 0));
    }

    //void UpdateMissile

    void SpawnMissile()
    {
        var newMissile = Instantiate(missile, transform.position, Quaternion.identity);
        spawnedMissiles.Add(newMissile);
    }
}
