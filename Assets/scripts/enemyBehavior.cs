using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour {

    private float startTime;
    private float speed;
    private float journeyLength;
    private Vector3 startPos;
    private Vector3 endPos;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        speed = 5;
        startPos = this.transform.position;
        endPos  = new Vector3(Random.Range(-20.0f, 20.0f), -1, 0);
        journeyLength = Vector3.Distance(startPos, endPos);
    }
	
	// Update is called once per frame
	void Update () {
        Move();

        if (IsCollidingWithGround()) {
            DecreaseHealth();
            DestroyEnemy();
        }
    }

    void Move() {

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        this.transform.position = Vector3.Lerp(startPos, endPos, fracJourney);

        //this.transform.position += new Vector3(0.0f, 1.0f, 0.0f) * -5.0f * Time.deltaTime;
    }

    bool IsCollidingWithGround() {
        return this.transform.position.y < 0;
    }

    public void DestroyEnemy() {
        Destroy(this.gameObject);


    }

    void DecreaseHealth() {
        var scoreHandler = GameObject.FindObjectOfType<ScoreHandlerBehavior>();
        var shb = scoreHandler.GetComponent<ScoreHandlerBehavior>();
        shb.ChangeHealth(-1);
    }
}
