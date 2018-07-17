using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var xPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0));
        this.gameObject.transform.position = xPos;
        this.gameObject.transform.localScale = new Vector3(1, 1000, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(collision.gameObject);
    }
}
