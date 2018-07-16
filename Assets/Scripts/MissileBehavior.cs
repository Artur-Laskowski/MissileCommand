using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour {
    
    public GameObject missileExplosion;

    private GameObject targetMarkerObject;
    
    public GameObject targetMarkerPrefab;

    private Vector3 startPos;

    private float speed;
    private float accuracy;
    private float explosionSize;
    private Vector3 targetPos;
    private bool initialized; //to avoid Update running before Initialize

    private float detonationDistance;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    // Use this for initialization
    void Start () {
        targetPos = GameObject.Find("target").transform.position;
        Destroy(gameObject, 5);

        startPos = this.transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, targetPos);

        detonationDistance = Settings.Instance.DefaultDetonationDistance;

        targetMarkerObject = CreateTargetMarker();
    }
	
	// Update is called once per frame
	void Update () {
        if (!initialized) return;

        MoveMissileToTarget();
        
        if (IsInRangeOfTarget()) {
            Detonate();
        }
    }

    public void Initialize(float missileSpeed, float accuracy, float explosionSize) {
        this.speed = missileSpeed;
        this.accuracy = accuracy;
        this.explosionSize = explosionSize;
        this.initialized = true;
    }

    GameObject CreateTargetMarker() {
        var distance = Vector3.Distance(startPos, targetPos);
        float inaccuracyOffsetAtDistance = accuracy * distance;
        targetPos = RandomizePosition(targetPos, inaccuracyOffsetAtDistance, inaccuracyOffsetAtDistance);

        GameObject targetMarkerObject = Instantiate(targetMarkerPrefab, targetPos, Quaternion.identity);
        return targetMarkerObject;
    }
    
    Vector3 RandomizePosition(Vector3 input, float xMaxOffset, float yMaxOffset) {
        float xOffset = -xMaxOffset / 2.0f + Random.Range(0, xMaxOffset);
        float yOffset = -yMaxOffset / 2.0f + Random.Range(0, yMaxOffset);
        input = input + new Vector3(xOffset, yOffset, 0);
        return input;
    }

    void MoveMissileToTarget() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        this.transform.position = Vector3.Lerp(startPos, targetPos, fracJourney);
    }

    void Detonate() {
        Destroy(gameObject);
        Destroy(targetMarkerObject);
        var explosion = Instantiate(missileExplosion, transform.position, Quaternion.identity);
        explosion.GetComponent<MissileExplosionBehavior>().Initialize(explosionSize);
    }

    bool IsInRangeOfTarget() {
        var distance = Vector3.Distance(this.transform.position, targetPos);
        return distance < detonationDistance;
    }
}
