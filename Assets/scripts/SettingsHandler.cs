using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour {

    private float speed;
    public float TargetMovementSpeed { get; private set; }


    static private SettingsHandler _instance;
    static public SettingsHandler Instance {
        get {
            if (_instance == null)
                throw new System.Exception("Tried to access singleton without instance");
            return _instance;
        }
        private set {
            if (_instance == null) {
                _instance = value;
            }
            else {
                Destroy(value.gameObject);
                throw new System.Exception("Second instance of singleton detected");
            }
        }
    }

    // Use this for initialization
    void Start () {
        Instance = this;
        InitializeWithDefaults();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeWithDefaults() {
        TargetMovementSpeed = 0.4f;
    }
}
