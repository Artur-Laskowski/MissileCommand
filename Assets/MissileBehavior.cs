using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour {
    
    public GameObject missileExplosion;
    public Vector3 TargetPos { private get; set; }
    
    public GameObject TargetMarkerObject { get; set; }

    private Vector3 startPos;

    public float speed;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    // Use this for initialization
    void Start () {
        TargetPos = GameObject.Find("target").transform.position;
        Destroy(gameObject, 5);
        startPos = this.transform.position;

        startTime = Time.time;
        speed = 5.0f;
        
        journeyLength = Vector3.Distance(startPos, TargetPos);
    }
	
	// Update is called once per frame
	void Update () {
        //var change = this.transform.rotation * Vector3.up;
        //this.transform.position += change * 0.05f;

        float distCovered = (Time.time - startTime) * speed;
        
        float fracJourney = distCovered / journeyLength;

        this.transform.position = Vector3.Lerp(startPos, TargetPos, fracJourney);

        var distance = Vector3.Distance(this.transform.position, TargetPos);
        
        if (distance < 0.1f)
            Detonate();
    }

    void Detonate() {
        Destroy(gameObject);
        Destroy(TargetMarkerObject);
        Instantiate(missileExplosion, transform.position, Quaternion.identity);
    }
}
