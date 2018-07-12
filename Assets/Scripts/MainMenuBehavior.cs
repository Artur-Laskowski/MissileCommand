using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuBehavior : MonoBehaviour {

    public GameObject newGameButton;

    public int enemyCount;

    static private MainMenuBehavior _instance;
    static public MainMenuBehavior Instance {
        get {
            //if (_instance == null)
                //_instance = new MainMenuBehavior();
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
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        }

        //DontDestroyOnLoad(this);
        
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        newGameButton.GetComponent<Button>().onClick.AddListener(
            () => {
                //newGameButton.SetActive(false);
                SceneManager.LoadScene("SampleScene");
            }
        );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
