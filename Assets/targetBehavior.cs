using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetBehavior : MonoBehaviour {

    private float speed;

	// Use this for initialization
	void Start () {
        speed = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 transform = new Vector3();
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform += new Vector3(-1.0f * speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform += new Vector3(1.0f * speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform += new Vector3(0, -1.0f * speed, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform += new Vector3(0, 1.0f * speed, 0);
        }

        this.transform.position += transform;


        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        this.transform.position = pos;
    }
}
