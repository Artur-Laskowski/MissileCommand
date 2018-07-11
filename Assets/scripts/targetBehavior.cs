using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour {


    private bool IsKeyboardInputMethod;

	// Use this for initialization
	void Start () {
        IsKeyboardInputMethod = false;
	}
	
	// Update is called once per frame
	void Update () {
        var newCursorPos = GetNewCursorLocation();

        this.transform.position = newCursorPos;
    }

    Vector3 GetNewCursorLocation() {
        var speed = SettingsHandler.Instance.TargetMovementSpeed;

        Vector3 position = new Vector3();
        if (IsKeyboardInputMethod) {
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
            position += transform;
        }
        else {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
        }
        return position;
    }
}
