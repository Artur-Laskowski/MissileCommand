using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementKey { Up, Down, Right, Left };
public delegate void ButtonPressedEventHandler();
public delegate void MovementKeyPressedEventHandler(MovementKey keyPressed);
public delegate void MouseMovedEventHandler(Vector3 cursorPosition);

public class UserControls : MonoBehaviour {

    public event ButtonPressedEventHandler PrimaryFire;
    public event ButtonPressedEventHandler PrimaryFireHeld;
    public event MovementKeyPressedEventHandler MovementKeyPressed;
    public event MouseMovedEventHandler MouseMoved;

    static private UserControls _instance;
    static public UserControls Instance {
        get {
            //if (_instance == null)
            //_instance = new Settings();
            //throw new System.Exception("Tried to access singleton without instance");
            return _instance;
        }
        private set {
            if (_instance == null) {
                _instance = value;
            }
            else {
                throw new System.Exception("Second instance of singleton detected");
            }
        }
    }

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update() {
        HandleButtons();
        HandleMouseMovement();

        Vector3 position = new Vector3();
        if (Settings.Instance.IsKeyboardInputMethod) {
            Vector3 transform = new Vector3();

            position += transform;
        }
        else {
        }
    }

    void HandleButtons() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            OnPrimaryFire();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) {
            OnPrimaryFireHeld();
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            OnMovementKeyPressed(MovementKey.Up);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            OnMovementKeyPressed(MovementKey.Down);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            OnMovementKeyPressed(MovementKey.Left);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            OnMovementKeyPressed(MovementKey.Right);
        }
    }

    void HandleMouseMovement() {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        OnMouseMoved(position);
    }

    void OnPrimaryFire() {
        if (PrimaryFire != null)
            PrimaryFire();
    }

    void OnPrimaryFireHeld() {
        if (PrimaryFireHeld != null)
            PrimaryFireHeld();
    }

    void OnMovementKeyPressed(MovementKey key) {
        if (MovementKeyPressed != null)
            MovementKeyPressed(key);
    }

    void OnMouseMoved(Vector3 position) {
        if (MouseMoved != null)
            MouseMoved(position);
    }
}
