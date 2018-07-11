using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    private float startTime;
    private float speed;
    private float journeyLength;
    private Vector3 startPos;
    private Vector3 endPos;

    ScoreHandlerBehavior scoreHandler;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        speed = 5;
        startPos = this.transform.position;
        endPos  = new Vector3(Random.Range(-20.0f, 20.0f), -1, 0);
        journeyLength = Vector3.Distance(startPos, endPos);

        scoreHandler = ScoreHandlerBehavior.Instance;
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
    }

    bool IsCollidingWithGround() {
        return this.transform.position.y < 0;
    }

    void DestroyEnemy() {
        Destroy(this.gameObject);
    }

    public void ExplodeEnemy() {
        DestroyEnemy();
        scoreHandler.ChangeScore(+1);
    }

    void DecreaseHealth() {
        scoreHandler.ChangeHealth(-1);
    }
}
