using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileShot : MonoBehaviour {

    public GameObject missile;
    public GameObject missileLauncher;
    public GameObject targetMarker;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            SpawnMissile();
        }
    }

    void SpawnMissile()
    {
        GameObject missileObject = Instantiate(missile, transform.position, missileLauncher.transform.rotation);
        MissileBehavior mb = missileObject.GetComponent<MissileBehavior>();
        mb.TargetMarkerObject = CreateTargetMarker();
    }

    GameObject CreateTargetMarker() {
        Transform targetTransform = GameObject.Find("target").transform;
        GameObject targetMarkerObject = Instantiate(targetMarker, targetTransform.position, Quaternion.identity);
        return targetMarkerObject;
    }
}
