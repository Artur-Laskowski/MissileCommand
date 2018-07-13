using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour {

    Vector3 currentLocation;

    private void Awake() {
        currentLocation = this.transform.position;
    }

    // Use this for initialization
    void Start () {
        if (Settings.Instance.IsKeyboardInputMethod) {
            UserControls.Instance.MovementKeyPressed += HandleMovementKey;
        }
        else {
            UserControls.Instance.MouseMoved += HandleMouseMovement;
        }
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = currentLocation;
    }

    void HandleMovementKey(MovementKey keyPressed) {
        float movement = Settings.Instance.TargetMovementSpeed;
        switch (keyPressed) {
            case MovementKey.Up:
                currentLocation.y += movement;
                break;
            case MovementKey.Down:
                currentLocation.y -= movement;
                break;
            case MovementKey.Left:
                currentLocation.x -= movement;
                break;
            case MovementKey.Right:
                currentLocation.x += movement;
                break;
        }
    }

    void HandleMouseMovement(Vector3 position) {
        currentLocation = position;
    }
}
