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
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnMissile();
        }
    }

    //void UpdateMissile

    void SpawnMissile()
    {
        GameObject o = Instantiate(missile, transform.position, missileLauncher.transform.rotation);
        Transform targetTransform = GameObject.Find("target").transform;
        GameObject targetMarkerObject = Instantiate(targetMarker, targetTransform.position, Quaternion.identity);
        MissileBehavior mb = o.GetComponent<MissileBehavior>();
        mb.TargetMarkerObject = targetMarkerObject;
    }
}
