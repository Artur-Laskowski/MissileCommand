using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTubeBehavior : MonoBehaviour {

    public GameObject target;
    public GameObject parentLauncher;

    public Transform muzzle;

    public GameObject missile;
    public GameObject targetMarker;

    private int roundsPerMinute;
    private float inaccuracyOffset;
    private float inaccuracyDistance;

    private float lastShotTime;

    // Use this for initialization
    void Start () {
        this.transform.SetParent(parentLauncher.transform);

        roundsPerMinute = Settings.Instance.DefaultRoundsPerMinute;
        inaccuracyOffset = Settings.Instance.DefaultInaccuracyOffset;
        inaccuracyDistance = Settings.Instance.InaccuracyDistance;
        lastShotTime = Time.time;
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
        float interval = 60.0f / roundsPerMinute;
        return Time.time > lastShotTime + interval;
    }

    void HandleUserInput() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            lastShotTime = Time.time - Random.Range(0.0f, 60.0f / roundsPerMinute);
        }

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

        lastShotTime = Time.time;
    }

    GameObject CreateTargetMarker() {
        Vector3 targetPosition = GameObject.Find("target").transform.position;
        var distance = Vector3.Distance(muzzle.position, targetPosition);
        float inaccuracyOffsetAtDistance = inaccuracyOffset * distance / inaccuracyDistance;
        targetPosition = RandomizePosition(targetPosition, inaccuracyOffsetAtDistance, inaccuracyOffsetAtDistance);

        GameObject targetMarkerObject = Instantiate(targetMarker, targetPosition, Quaternion.identity);
        return targetMarkerObject;
    }

    Vector3 RandomizePosition(Vector3 input, float xMaxOffset, float yMaxOffset) {
        float xOffset = -xMaxOffset / 2.0f + Random.Range(0, xMaxOffset);
        float yOffset = -yMaxOffset / 2.0f + Random.Range(0, yMaxOffset);
        input = input + new Vector3(xOffset, yOffset, 0);
        return input;
    }
}
