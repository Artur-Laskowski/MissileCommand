using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    
    public float TargetMovementSpeed { get; private set; }
    public float EnemySpawnsPerMinute { get; private set; }
    public int MaxHealth { get; private set; }
    public bool IsLowFrameMode { get; private set; }

    public int DefaultRoundsPerMinute { get; private set; }
    public float DefaultInaccuracyOffset { get; private set; }
    public float InaccuracyDistance { get; private set; }

    public float DefaultMaxExplosionSize { get; private set; }
    public float DefaultExplosionDuration { get; private set; }
    public float DefaultExplosionCollapseRate { get; private set; }

    public float DefaultMissileSpeed { get; private set; }
    public float DefaultDetonationDistance { get; private set; }

    public bool IsKeyboardInputMethod { get; private set; }

    static private Settings _instance;
    static public Settings Instance {
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
        EnemySpawnsPerMinute = 20;
        MaxHealth = 100;
        IsLowFrameMode = false;

        DefaultRoundsPerMinute = 100;
        DefaultInaccuracyOffset = 3.0f;
        InaccuracyDistance = 10.0f;

        DefaultMaxExplosionSize = 10.0f;
        DefaultExplosionDuration = 1.0f;
        DefaultExplosionCollapseRate = 1.0f / 4.0f;

        DefaultMissileSpeed = 10.0f;
        DefaultDetonationDistance = 0.1f;

        IsKeyboardInputMethod = false;
    }
}
