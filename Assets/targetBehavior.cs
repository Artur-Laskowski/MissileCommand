using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 transform = new Vector3();
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform += new Vector3(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform += new Vector3(0, -0.1f, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform += new Vector3(0, 0.1f, 0);
        }

        this.transform.position += transform;

    }
}
