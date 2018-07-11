using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour {
    
    public GameObject missileExplosion;
    public Vector3 TargetPos { private get; set; }
    
    public GameObject TargetMarkerObject { get; set; }

    private Vector3 startPos;

    public float speed;

    private float detonationDistance;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    // Use this for initialization
    void Start () {
        TargetPos = TargetMarkerObject.transform.position;
        Destroy(gameObject, 5);

        startPos = this.transform.position;
        startTime = Time.time;
        speed = Settings.Instance.DefaultMissileSpeed;
        journeyLength = Vector3.Distance(startPos, TargetPos);

        detonationDistance = Settings.Instance.DefaultDetonationDistance;
    }
	
	// Update is called once per frame
	void Update () {
        MoveMissileToTarget();
        
        if (IsInRangeOfTarget()) {
            Detonate();
        }
    }

    void MoveMissileToTarget() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        this.transform.position = Vector3.Lerp(startPos, TargetPos, fracJourney);
    }

    void Detonate() {
        Destroy(gameObject);
        Destroy(TargetMarkerObject);
        Instantiate(missileExplosion, transform.position, Quaternion.identity);
    }

    bool IsInRangeOfTarget() {
        var distance = Vector3.Distance(this.transform.position, TargetPos);
        return distance < detonationDistance;
    }
}
