using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileTubeBehavior : MonoBehaviour {

    public GameObject target;
    public GameObject parentLauncher;

	// Use this for initialization
	void Start () {
        this.transform.SetParent(parentLauncher.transform);
       
	}
	
	// Update is called once per frame
	void Update () {
        SetPos(transform.position, target.transform.position);
    }

    void SetPos(Vector3 start, Vector3 end) {
        var dir = end - start;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }
}
