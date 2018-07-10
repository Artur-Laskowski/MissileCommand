using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileExplosionBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale += new Vector3(0.1f, 0.1f, 0.0f);
	}
}
