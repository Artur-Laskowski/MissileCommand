using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTubeBehavior : MonoBehaviour {

    public GameObject target;
    public GameObject parentLauncher;

    public Transform muzzle;

    public GameObject missile;
    public GameObject targetMarker;

    public float LastShotTime { get; set; }
    public int RoundsPerMinute { get; set; }

    // Use this for initialization
    void Start () {
        this.transform.SetParent(parentLauncher.transform);

        LastShotTime = Time.time;
        RoundsPerMinute = 6000;
	}
	
	// Update is called once per frame
	void Update () {
        SetRotation(transform.position, target.transform.position);

        HandleUserInput();
    }

    void SetRotation(Vector3 start, Vector3 end) {
        var dir = end - start;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    bool CanSpawnMissile() {
        float interval = 60.0f / RoundsPerMinute;
        return Time.time > LastShotTime + interval;
    }

    void HandleUserInput() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) {
            if (CanSpawnMissile()) {
                SpawnMissile();
            }
        }
    }

    void SpawnMissile() {
        GameObject missileObject = Instantiate(missile, muzzle.position, this.transform.rotation);
        MissileBehavior mb = missileObject.GetComponent<MissileBehavior>();
        mb.TargetMarkerObject = CreateTargetMarker();

        LastShotTime = Time.time;
    }

    GameObject CreateTargetMarker() {
        Transform targetTransform = GameObject.Find("target").transform;
        GameObject targetMarkerObject = Instantiate(targetMarker, targetTransform.position, Quaternion.identity);
        return targetMarkerObject;
    }
}
