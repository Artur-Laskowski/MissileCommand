using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    
    public float TargetMovementSpeed { get; private set; }
    public float EnemySpawnsPerMinute { get; private set; }
    public int MaxHealth { get; private set; }
    public bool IsLowFrameMode { get; private set; }

    public int DefaultRoundsPerMinute { get; private set; }

    public float DefaultMaxExplosionSize { get; private set; }
    public float DefaultExplosionDuration { get; private set; }
    public float DefaultExplosionCollapseRate { get; private set; }
    
    public float DefaultDetonationDistance { get; private set; }

    public bool IsKeyboardInputMethod { get; private set; }

    public float EnemySmallDestructibleAfter { get; private set; }

    public int DefaultMoney { get; private set; }

    public float XPosMin { get; private set; }
    public float XPosMax { get; private set; }

    public int SubEnemiesCount { get; set; }

    public float[] projectileSpeedLevels;
    public float[] explosionSizeLevels;
    public float[] accuracyLevels;
    public int[] rofLevels;
    public int[] upgradeCosts;

    //public set
    static private Settings _instance;
    static public Settings Instance {
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
    
    void Awake () {
        InitializeWithDefaults();
        Instance = this;
    }

    void InitializeWithDefaults() {
        TargetMovementSpeed = 0.4f;
        EnemySpawnsPerMinute = 200;
        MaxHealth = 100;
        IsLowFrameMode = false;

        DefaultRoundsPerMinute = 200;

        DefaultMaxExplosionSize = 10.0f;
        DefaultExplosionDuration = 1.0f;
        DefaultExplosionCollapseRate = 1.0f / 4.0f;
        
        DefaultDetonationDistance = 0.1f;

        DefaultMoney = 1000;
        SubEnemiesCount = 2;

        IsKeyboardInputMethod = PlayerPrefs.GetInt("InputMethod", 0) == 1;

        EnemySmallDestructibleAfter = 0.5f;

        projectileSpeedLevels = new float[] { 10, 12, 15, 18, 22, 26, 30, 35, 40, 50 };
        explosionSizeLevels = new float[] { 4, 5, 6, 7, 8, 9, 10, 12, 15, 20 };
        accuracyLevels = new float[] { 0.1f, 0.09f, 0.08f, 0.07f, 0.06f, 0.05f, 0.04f, 0.03f, 0.02f, 0.01f, 0.0f };
        rofLevels = new int[] { 200, 250, 300, 400, 500, 600, 800, 1000, 1200, 1500 };
        upgradeCosts = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90};

        XPosMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x;
        XPosMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
    }
}
